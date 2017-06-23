using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            sampleLog();

            Console.WriteLine("Press any key to exit ...");
            Console.Read();
        }

        private static void sampleLog()
        {
            string method = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            Mora.Logger.ILogger.ILog dbLogger = new Mora.Logger.MsSqlLogger.DbLogger()
            {
                CanAddError = true,
                CanAddInfo = true,
                CanAddWarning = true
            };

            Mora.Logger.ILogger.ILog consoleLogger = new Mora.Logger.ConsoleLogger.SimpleLogger()
            {
                CanAddError = true,
                CanAddInfo = true,
                CanAddWarning = true
            };

            Mora.Logger.ILogger.LogModel model = new Mora.Logger.ILogger.LogModel() {
                Counter = 1,
                Group = Guid.NewGuid(),
                LogType = Mora.Logger.ILogger.TypeOfLog.Info,
                ReferenceName = "TestID",
                ReferenceValue = "AAA",
                What = "Test A",
                When = DateTime.Now,
                Who = Mora.Logger.LoggerHelper.Utility.GetWho("SampleConsole.Program", method, "0.0.0.0")
            };

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    Guid result1 = consoleLogger.Log(model);


                    Guid result2 = dbLogger.Log(model);

                    Console.WriteLine("result1:{0}, result2:{1}", result1.ToString(), result2.ToString());

                    model.Counter++;
                }
                catch(System.Data.Entity.Validation.DbEntityValidationException vex)
                {
                    int count = vex.EntityValidationErrors.Count();

                    if (count > 0)
                    {
                        foreach (var error in vex.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity:{0}", error.Entry.Entity.GetType().ToString());

                            foreach (var item in error.ValidationErrors)
                            {
                                Console.WriteLine("PropertyName:{0} | ErrorMessage:{1}", item.PropertyName, item.ErrorMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

        }
    }
}
