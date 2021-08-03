using System;
using System.Text;
using Intel.Dal;


namespace passwordVault2Host
{
    class Program
    {
        static void Main(string[] args)
        {
#if AMULET
            // When compiled for Amulet the Jhi.DisableDllValidation flag is set to true 
			// in order to load the JHI.dll without DLL verification.
            // This is done because the JHI.dll is not in the regular JHI installation folder, 
			// and therefore will not be found by the JhiSharp.dll.
            // After disabling the .dll validation, the JHI.dll will be loaded using the Windows search path
			// and not by the JhiSharp.dll (see http://msdn.microsoft.com/en-us/library/7d83bc18(v=vs.100).aspx for 
			// details on the search path that is used by Windows to locate a DLL) 
            // In this case the JHI.dll will be loaded from the $(OutDir) folder (bin\Amulet by default),
			// which is the directory where the executable module for the current process is located.
            // The JHI.dll was placed in the bin\Amulet folder during project build.
            Jhi.DisableDllValidation = true;
#endif

            Jhi jhi = Jhi.Instance;
            JhiSession session;

            // This is the UUID of this Trusted Application (TA).
            //The UUID is the same value as the applet.id field in the Intel(R) DAL Trusted Application manifest.
            string appletID = "3e1dbd8c-b1a6-43be-9138-cc150bf454e7";
            // This is the path to the Intel Intel(R) DAL Trusted Application .dalp file that was created by the Intel(R) DAL Eclipse plug-in.


            // ****************** CHANGE IF NEED ******************
            string appletPath = "C:\\Users\\LENOVO\\eclipse-workspace\\passwordVault2\\bin\\PasswordVault2.dalp";
            // ****************** CHANGE IF NEED ******************

            // Install the Trusted Application
            jhi.Install(appletID, appletPath);

            // Start a session with the Trusted Application
            byte[] initBuffer = new byte[] { }; // Data to send to the applet onInit function
            // Opening a session
            jhi.CreateSession(appletID, JHI_SESSION_FLAGS.None, initBuffer, out session);

            LoginWin lw = new LoginWin(jhi, session);
            lw.ShowDialog();

            // Close the session
            jhi.CloseSession(session);

            // Uninstall the Trusted Application
            jhi.Uninstall(appletID);
        }
    }
}