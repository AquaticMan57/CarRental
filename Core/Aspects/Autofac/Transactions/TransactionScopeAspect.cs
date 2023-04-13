using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transactions
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
			using (TransactionScope scope = new TransactionScope())
			{
                try
                {
                    invocation.Proceed();
                    scope.Complete();

                }
                catch (Exception e)
                {
                    scope.Dispose();
                    throw new Exception("Transaction error has been found!!");
                }
            }
			
        }
    }
}
