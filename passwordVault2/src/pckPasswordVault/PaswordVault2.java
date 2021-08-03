package pckPasswordVault;

import com.intel.util.*;
import com.intel.crypto.Random;
import com.intel.crypto.SymmetricBlockCipherAlg;

//
// Implementation of DAL Trusted Application: passwordVault2 
//
// **************************************************************************************************
// NOTE:  This default Trusted Application implementation is intended for DAL API Level 7 and above
// **************************************************************************************************

public class PaswordVault2 extends IntelApplet {

	private final int CMD_SaveSeed = 1;
	private final int CMD_SaveMainPassword = 2;
	private final int CMD_GetDecPassword = 3;
	private final int CMD_GetEncPassword = 4;
	private final int CMD_CheckPassword = 5;
	private final int CMD_CheckTimeLimit = 9;
	private final int CMD_stopTimeLimit = 10;

	private final int SIZE_key = 32;
	private final int SIZE_pass = 16;
	private boolean flagLenReq = false;
	private boolean flagTrPass = false;

	private SymmetricBlockCipherAlg sAlg;

	private final int TIME_SEC = 120;
	private int counter = TIME_SEC;
	private TimerManager.Timer timer;
	private TimerClient timerClient;
	private boolean timeLimit = false;

	/**
	 * This method will be called by the VM when a new session is opened to the
	 * Trusted Application and this Trusted Application instance is being
	 * created to handle the new session. This method cannot provide response
	 * data and therefore calling setResponse or setResponseCode methods from it
	 * will throw a NullPointerException.
	 * 
	 * @param request
	 *            the input data sent to the Trusted Application during session
	 *            creation
	 * 
	 * @return APPLET_SUCCESS if the operation was processed successfully, any
	 *         other error status code otherwise (note that all error codes will
	 *         be treated similarly by the VM by sending "cancel" error code to
	 *         the SW application).
	 */
	public int onInit(byte[] request) {
		sAlg = SymmetricBlockCipherAlg
				.create(SymmetricBlockCipherAlg.ALG_TYPE_AES_ECB);
		return APPLET_SUCCESS;
	}

	/**
	 * This method will be called by the VM to handle a command sent to this
	 * Trusted Application instance.
	 * 
	 * @param commandId
	 *            the command ID (Trusted Application specific)
	 * @param request
	 *            the input data for this command
	 * @return the return value should not be used by the applet
	 */
	public int invokeCommand(int commandId, byte[] request1) {
		flagLenReq = false;

		if (request1.length <= 16)
			flagLenReq = true;

		byte[] myResponse = { 'O', 'K' };
		byte[] request = new byte[SIZE_pass];

		com.intel.langutil.ArrayUtils.copyByteArray(request1, 0, request, 0,
				request1.length);

		if (commandId == CMD_SaveSeed && flagLenReq)
			myResponse = createSeed().getBytes();
		
		if (commandId == CMD_SaveMainPassword && flagLenReq) {
			byte[] passToEnc = new byte[SIZE_pass];
			passToEnc = getEncPassword(request);
			storedFlash(passToEnc, 1);
		}
		if (commandId == CMD_GetDecPassword && flagLenReq && timeLimit)
			myResponse = getDecPassword(request);
		if (commandId == CMD_GetEncPassword && flagLenReq && timeLimit)
			myResponse = getEncPassword(request);
		if (commandId == CMD_CheckTimeLimit && flagLenReq) {
			if (timeLimit)
				myResponse = "LimitOk".getBytes();
			else
				myResponse = "ErrorLimit".getBytes();
		}
		if (commandId == CMD_stopTimeLimit && flagLenReq) {
			timer.destroy();
			flagTrPass = false;
		}

		if (commandId == CMD_CheckPassword && flagLenReq) {
			if (checkPass(request)) {
				myResponse = "true".getBytes();
				timerClient = new TimerClient() {

					public void onTimerTick(byte[] arg0) {
						if (counter > 0) {
							counter--;
						} else {
							timeLimit = false;
							flagTrPass = false;
							counter = TIME_SEC;
							timer.destroy();
						}
					}
				};
				final byte[] tp = { 't', 'b' };
				timer = TimerManager.getInstance().createTimer(timerClient);
				timer.start(1000, tp, 0, tp.length, true);
			} else {
				myResponse = "false".getBytes();
			}
		}

		/*
		 * To return the response data to the command, call the setResponse
		 * method before returning from this method. Note that calling this
		 * method more than once will reset the response data previously set.
		 */
		setResponse(myResponse, 0, myResponse.length);

		/*
		 * In order to provide a return value for the command, which will be
		 * delivered to the SW application communicating with the Trusted
		 * Application, setResponseCode method should be called. Note that
		 * calling this method more than once will reset the code previously
		 * set. If not set, the default response code that will be returned to
		 * SW application is 0.
		 */
		setResponseCode(commandId);

		/*
		 * The return value of the invokeCommand method is not guaranteed to be
		 * delivered to the SW application, and therefore should not be used for
		 * this purpose. Trusted Application is expected to return
		 * APPLET_SUCCESS code from this method and use the setResposeCode
		 * method instead.
		 */
		return APPLET_SUCCESS;
	}

	private String createSeed() {
		byte[] temp = new byte[SIZE_key];
		try {
			FlashStorage.readFlashData(0, temp, 0);
			sAlg.setKey(temp, (short) 0, (short) temp.length);
			return "true";
		} catch (Exception e) {
			Random.getRandomBytes(temp, (short) 0, (short) temp.length);
			FlashStorage.writeFlashData(0, temp, 0, temp.length);
			sAlg.setKey(temp, (short) 0, (short) temp.length);
			return "false";
		}
	}

	private byte[] getEncPassword(byte[] request) {
		try {
			byte[] tempPass = new byte[SIZE_pass];
			sAlg.encryptComplete(request, (short) 0, (short) request.length,
					tempPass, (short) 0);
			return tempPass;
		} catch (Exception e) {	}
		return null;
	}

	private byte[] getDecPassword(byte[] request) {
		try {
			if (!flagTrPass)
				return null;
			byte[] tempPass = new byte[SIZE_pass];
			sAlg.decryptComplete(request, (short) 0, (short) request.length,
					tempPass, (short) 0);
			return tempPass;
		} catch (Exception e) { }
		return null;
	}

	private void storedFlash(byte[] arrToStored, int numFile) {
		DebugPrint.printBuffer(arrToStored);
		try {
			byte[] temp = new byte[SIZE_pass];
			FlashStorage.readFlashData(numFile, temp, 0);
		} catch (Exception e) {
			FlashStorage.writeFlashData(numFile, arrToStored, 0,
					arrToStored.length);
		}
	}

	private boolean checkPass(byte[] request) {
		byte[] passToEnc = getEncPassword(request);

		byte[] mPass1 = new byte[SIZE_pass];
		try {
			FlashStorage.readFlashData(1, mPass1, 0);
		} catch (Exception e) {
			return false;
		}

		boolean flag = true;
		int rSize1 = realSizeOfByteAarry(mPass1);
		int rSize2 = realSizeOfByteAarry(passToEnc);

		if (!(rSize1 == rSize2))
			flag = false;
		else
			for (int i = 0; i < mPass1.length; i++)
				if (mPass1[i] != passToEnc[i])
					flag = false;

		if (flag) {
			flagTrPass = true;
			timeLimit = true;
		}

		return flag;
	}

	private int realSizeOfByteAarry(byte[] arr) {
		int cnt = 0;
		for (byte b : arr) {
			if (b != 0x00) {
				cnt++;
			} else {
				break;
			}
		}
		return cnt;
	}

	/**
	 * This method will be called by the VM when the session being handled by
	 * this Trusted Application instance is being closed and this Trusted
	 * Application instance is about to be removed. This method cannot provide
	 * response data and therefore calling setResponse or setResponseCode
	 * methods from it will throw a NullPointerException.
	 * 
	 * @return APPLET_SUCCESS code (the status code is not used by the VM).
	 */
	public int onClose() {
		return APPLET_SUCCESS;
	}
}
