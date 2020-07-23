using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.Private.InfoCenterLib;
using Autodesk.Windows;

using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;

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

            if(!license.IsValid())
            {
                return new ResultBuffer(new TypedValue(Convert.ToInt32(LispDataType.Int32), -1));
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
