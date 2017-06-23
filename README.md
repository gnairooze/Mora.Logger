# Mora.Logger


It is a simple logger.
To use it just follow the following steps:
1. Download the recent release file [MsSqlLogger](https://github.com/gnairooze/Mora.Logger/raw/master/Releases/Mora.Logger.MsSqlLogger.1.0.7z) 
2. Run the SQL script in the release package to create logger table.
3. Reference the following DLLs:

    a. ILogger

    b. MsSqlLogger

    c. Entity Framework

4. In the executing application config file add a connection string named "LoggerDB" and add the connection string to the database that you created the logger table in it.
5. Follow the following sample code

`

            string method = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            Mora.Logger.ILogger.ILog dbLogger = new Mora.Logger.MsSqlLogger.DbLogger()
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
                    Guid result = dbLogger.Log(model);

                    Console.WriteLine("result:{0}", result.ToString());

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
`
