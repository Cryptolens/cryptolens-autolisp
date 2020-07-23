using System;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using SKM.V3;
using SKM.V3.Methods;


[assembly: CommandClass(typeof(autolisp_modules.MyCommands))]
namespace autolisp_modules
{
    public class MyCommands
    {
        [LispFunction("checklicensefile")]
        static public ResultBuffer checklicensefile_net(ResultBuffer args)
        {
            if (args == null)
            {
                throw new ArgumentException("No arguments provided.");
            }

            Array argsArray = null;
            argsArray = args.AsArray();

            if (argsArray.Length == 0)
            {
                throw new ArgumentException("No arguments provided.");
            }

            string rsaPublicKey = (string)((TypedValue)argsArray.GetValue(0)).Value;
            string pathToFile = (string)((TypedValue)argsArray.GetValue(1)).Value;

            var license = new LicenseKey().LoadFromFile(pathToFile).HasValidSignature(rsaPublicKey);

            if (license != null && license.MaxNoOfMachines > 0)
            {
                // check the machine code too only if node-locking is enabled.
                if (!Helpers.IsOnRightMachinePI(license, false, false))
                {
                    return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Int32), -1));
                }
            }

            var result = license.IsValid() ? 1 : -1;

            return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Int32), result));
        }

        [LispFunction("getexpirationdate")]
        static public ResultBuffer getexpirationdate_net(ResultBuffer args)
        {
            if (args == null)
            {
                throw new ArgumentException("No arguments provided.");
            }

            Array argsArray = null;
            argsArray = args.AsArray();

            if (argsArray.Length == 0)
            {
                throw new ArgumentException("No arguments provided.");
            }

            string rsaPublicKey = (string)((TypedValue)argsArray.GetValue(0)).Value;
            string pathToFile = (string)((TypedValue)argsArray.GetValue(1)).Value;

            var license = new LicenseKey().LoadFromFile(pathToFile).HasValidSignature(rsaPublicKey);

            if(license != null && license.MaxNoOfMachines > 0)
            {
                // check the machine code too only if node-locking is enabled.
                if (!Helpers.IsOnRightMachinePI(license, false, false))
                {
                    return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Int32), -1));
                }
            }

            if(!license.IsValid())
            {
                return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Int32), -1));
            }

            if (license != null && license.MaxNoOfMachines > 0)
            {
                // check the machine code too only if node-locking is enabled.
                if (!Helpers.IsOnRightMachinePI(license, false, false))
                {
                    return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Int32), -1));
                }
            }

            return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Int32), 
                (Int32)license.Expires.Subtract(new DateTime(1970, 1, 1)).TotalSeconds));
        }

        [LispFunction("getmachinecode")]
        static public ResultBuffer getmachinecode_net(ResultBuffer args)
        { 
            return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Text),
                Helpers.GetMachineCodePI()));
        }
    }
}
