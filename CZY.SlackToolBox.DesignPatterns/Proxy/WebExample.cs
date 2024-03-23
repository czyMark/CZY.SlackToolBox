using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Proxy
{
    #region 动态代理例子1
    //调用器 
    public class Invocation
    {
        public object[] Parameter { get; set; }
        public Delegate DelegateMethod { get; set; }
        public object Proceed()
        {
            return this.DelegateMethod.DynamicInvoke(Parameter);
        }
    }
    //拦截器
    public class Interceptor
    {
        public object Intercept(Invocation invocation)
        {
            Console.WriteLine("111111");
            return invocation.Proceed();
        }
    }
    //代理动态创建对象
    public class ProxyBuilder
    {
        //根据传进来的类型 返回对应的被代理类型
        public static Type BuildProxy(Type targetType)
        {
            Type result = null;
            if (targetType == typeof(Food))
            {
                result = typeof(FoodProxyExample);
            }
            else if (targetType == typeof(Meat))
            {
                result = typeof(MeatProxyExample);
            }
            return result;
        }
    }
    //食物被代理类
    public class Food
    {
        public virtual string Eat(int p1, int p2)
        {
            return "吃";
        }
    }
    //吃饭被代理类
    public class Meat
    {
        public virtual string Eat(int p1)
        {
            return "吃";
        }
    }
    //食物代理类
    public class FoodProxyExample : Food
    {
        public override string Eat(int p1, int p2)
        {


            #region 调用器 拦截器 方法调用
            //适用所有调用方法，不用每个都写
            //组织方法参数
            //object[] Parameter = new object[2];
            //Parameter[0] = p1;
            //Parameter[1] = p2;
            ////定义调用 父类的 委托方法
            //Func<int, int, string> DelegateMethod = base.Eat;
            ////调用器 
            //Invocation invocation = new Invocation();
            //invocation.Parameter = Parameter;
            //invocation.DelegateMethod = DelegateMethod;
            ////拦截器
            //Interceptor interceptor = new Interceptor();
            //return (string)interceptor.Intercept(invocation);

            #endregion

            #region 写死调用
            //写死只能调用父类的某个方法，如果是不同的需要在扩展写
            string str = "参数验证\r\n";
            str += base.Eat(p1, p2);
            str += "回收链接";
            return str;
            #endregion
        }
    }
    //吃饭代理类
    public class MeatProxyExample : Meat
    {
        public override string Eat(int p1)
        {
            object[] Parameter = new object[1];
            Parameter[0] = p1;
            Func<int, string> DelegateMethod = base.Eat;
            Invocation invocation = new Invocation();
            invocation.Parameter = Parameter;
            invocation.DelegateMethod = DelegateMethod;

            Interceptor interceptor = new Interceptor();
            return (string)interceptor.Intercept(invocation);
        }
    }
    #endregion

    #region 动态代理例子2
    /// <summary>
    /// 拦截器
    /// </summary>
    public interface IInterceptor
    {
        object Intercept(Invocation invocation);
    }
    /// <summary>
    /// 为要拦截的方法打上标记
    /// </summary>
    public class RewriteAttribute : System.Attribute
    {
    }

    /// <summary>
    /// 代理生成类
    /// </summary>
    public class ProxyBuilder2<T> where T : IInterceptor, new()
    {
        protected static AssemblyName DemoName = new AssemblyName("DynamicAssembly");
        /// <summary>
        /// 在内存中保存好存放代理类的动态程序集
        /// </summary>
        protected static AssemblyBuilder assyBuilder = AssemblyBuilder.DefineDynamicAssembly(DemoName, AssemblyBuilderAccess.Run);
        /// <summary>
        /// 在内存中保存好存放代理类的托管模块
        /// </summary>
        protected static ModuleBuilder modBuilder = assyBuilder.DefineDynamicModule(DemoName.Name);
        /// <summary>
        /// 类型类的类型 动态构造targetType的代理类
        /// </summary>
        /// <returns></returns>
        public static Type BuildProxy(Type targetType, bool declaredOnly = false)
        {
            //创建一个类型 
            if (targetType.IsInterface)
            {
                throw new Exception("cannot create a proxy class for the interface");
            }
            Type TypeOfParent = targetType;
            Type[] TypeOfInterfaces = new Type[0];
            TypeBuilder typeBuilder = modBuilder.DefineType(targetType.Name + "Proxy" + Guid.NewGuid().ToString("N"), TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.BeforeFieldInit, TypeOfParent, TypeOfInterfaces);
            BindingFlags bindingFlags;
            if (declaredOnly)
            {
                bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            }
            else
            {
                bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            }
            MethodInfo[] targetMethods = targetType.GetMethods(bindingFlags);
            //遍历各个方法
            foreach (MethodInfo targetMethod in targetMethods)
            {
                //只挑出virtual的实例方法进行重写
                //只挑出打了RewriteAttribute标记的方法进行重写
                if (targetMethod.IsVirtual && !targetMethod.IsStatic && !targetMethod.IsFinal && !targetMethod.IsAssembly && targetMethod.GetCustomAttributes(true).Any(e => (e as RewriteAttribute != null)))
                {
                    Type[] paramType;
                    Type returnType;
                    ParameterInfo[] paramInfo;
                    Type delegateType = GetDelegateType(targetMethod, out paramType, out returnType, out paramInfo);
                    Type interceptorType = typeof(T);
                    MethodBuilder methodBuilder = typeBuilder.DefineMethod(targetMethod.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.HideBySig, returnType, paramType);
                    for (var i = 0; i < paramInfo.Length; i++)
                    {
                        ParameterBuilder paramBuilder = methodBuilder.DefineParameter(i + 1, paramInfo[i].Attributes, paramInfo[i].Name);
                        if (paramInfo[i].HasDefaultValue)
                        {
                            paramBuilder.SetConstant(paramInfo[i].DefaultValue);
                        }
                    }
                    ILGenerator il = methodBuilder.GetILGenerator();
                    //下面的il相当于
                    //public class parent
                    //{
                    //    public virtual string test(List<string> p1, int p2)
                    //    {
                    //        return "123";
                    //    }
                    //}
                    //public class child : parent
                    //{
                    //    public override string test(List<string> p1, int p2)
                    //    {
                    //        object[] Parameter = new object[2];
                    //        Parameter[0] = p1;
                    //        Parameter[1] = p2;
                    //        Func<List<string>, int, string> DelegateMethod = base.test;

                    //        Invocation invocation = new Invocation();
                    //        invocation.Parameter = Parameter;
                    //        invocation.DelegateMethod = DelegateMethod;
                    //        Interceptor interceptor = new Interceptor();
                    //        return (string)interceptor.Intercept(invocation);
                    //    }
                    //}

                    Label label1 = il.DefineLabel();

                    il.DeclareLocal(typeof(object[]));
                    il.DeclareLocal(delegateType);
                    il.DeclareLocal(typeof(Invocation));
                    il.DeclareLocal(interceptorType);
                    LocalBuilder re = null;
                    if (returnType != typeof(void))
                    {
                        re = il.DeclareLocal(returnType);
                    }
                    il.Emit(OpCodes.Ldc_I4, paramType.Length);
                    il.Emit(OpCodes.Newarr, typeof(object));
                    il.Emit(OpCodes.Stloc, 0);
                    for (var i = 0; i < paramType.Length; i++)
                    {
                        il.Emit(OpCodes.Ldloc, 0);
                        il.Emit(OpCodes.Ldc_I4, i);
                        il.Emit(OpCodes.Ldarg, i + 1);
                        if (paramType[i].IsValueType)
                        {
                            il.Emit(OpCodes.Box, paramType[i]);
                        }
                        il.Emit(OpCodes.Stelem_Ref);
                    }
                    il.Emit(OpCodes.Ldarg, 0);
                    il.Emit(OpCodes.Ldftn, targetMethod);
                    il.Emit(OpCodes.Newobj, delegateType.GetConstructors()[0]);
                    il.Emit(OpCodes.Stloc, 1);
                    il.Emit(OpCodes.Newobj, typeof(Invocation).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(e => e.GetParameters().Length == 0));
                    il.Emit(OpCodes.Stloc, 2);
                    il.Emit(OpCodes.Ldloc, 2);
                    il.Emit(OpCodes.Ldloc, 0);
                    il.Emit(OpCodes.Callvirt, typeof(Invocation).GetMethod("set_Parameter"));
                    il.Emit(OpCodes.Ldloc, 2);
                    il.Emit(OpCodes.Ldloc, 1);
                    il.Emit(OpCodes.Callvirt, typeof(Invocation).GetMethod("set_DelegateMethod"));
                    il.Emit(OpCodes.Newobj, interceptorType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(e => e.GetParameters().Length == 0));
                    il.Emit(OpCodes.Stloc, 3);
                    il.Emit(OpCodes.Ldloc, 3);
                    il.Emit(OpCodes.Ldloc, 2);
                    il.Emit(OpCodes.Callvirt, interceptorType.GetMethod("Intercept"));
                    if (returnType != typeof(void))
                    {
                        il.Emit(OpCodes.Castclass, returnType);
                        il.Emit(OpCodes.Stloc_S, re);
                        il.Emit(OpCodes.Br_S, label1);
                        il.MarkLabel(label1);
                        il.Emit(OpCodes.Ldloc_S, re);
                    }
                    else
                    {
                        il.Emit(OpCodes.Pop);
                    }
                    il.Emit(OpCodes.Ret);
                }
            }
            //真正创建，并返回
            Type proxyType = typeBuilder.CreateType();
            return proxyType;
        }
        /// <summary>
        /// 通过MethodInfo获得其参数类型列表，返回类型，和委托类型
        /// </summary>
        /// <param name="targetMethod"></param>
        /// <param name="paramType"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public static Type GetDelegateType(MethodInfo targetMethod, out Type[] paramType, out Type returnType, out ParameterInfo[] paramInfo)
        {
            paramInfo = targetMethod.GetParameters();
            //paramType
            paramType = new Type[paramInfo.Length];
            for (int i = 0; i < paramInfo.Length; i++)
            {
                paramType[i] = paramInfo[i].ParameterType;
            }
            //returnType
            returnType = targetMethod.ReturnType;
            //delegateType
            Type delegateType;
            if (returnType == typeof(void))
            {
                switch (paramType.Length)
                {
                    case 0:
                        delegateType = typeof(Action);
                        break;
                    case 1:
                        delegateType = typeof(Action<>).MakeGenericType(paramType);
                        break;
                    case 2:
                        delegateType = typeof(Action<,>).MakeGenericType(paramType);
                        break;
                    case 3:
                        delegateType = typeof(Action<,,>).MakeGenericType(paramType);
                        break;
                    case 4:
                        delegateType = typeof(Action<,,,>).MakeGenericType(paramType);
                        break;
                    case 5:
                        delegateType = typeof(Action<,,,,>).MakeGenericType(paramType);
                        break;
                    case 6:
                        delegateType = typeof(Action<,,,,,>).MakeGenericType(paramType);
                        break;
                    case 7:
                        delegateType = typeof(Action<,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 8:
                        delegateType = typeof(Action<,,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 9:
                        delegateType = typeof(Action<,,,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 10:
                        delegateType = typeof(Action<,,,,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 11:
                        delegateType = typeof(Action<,,,,,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 12:
                        delegateType = typeof(Action<,,,,,,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 13:
                        delegateType = typeof(Action<,,,,,,,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 14:
                        delegateType = typeof(Action<,,,,,,,,,,,,,>).MakeGenericType(paramType);
                        break;
                    case 15:
                        delegateType = typeof(Action<,,,,,,,,,,,,,,>).MakeGenericType(paramType);
                        break;
                    default:
                        delegateType = typeof(Action<,,,,,,,,,,,,,,,>).MakeGenericType(paramType);
                        break;
                }
            }
            else
            {
                Type[] arr = new Type[paramType.Length + 1];
                for (int i = 0; i < paramType.Length; i++)
                {
                    arr[i] = paramType[i];
                }
                arr[paramType.Length] = returnType;
                switch (paramType.Length)
                {
                    case 0:
                        delegateType = typeof(Func<>).MakeGenericType(arr);
                        break;
                    case 1:
                        delegateType = typeof(Func<,>).MakeGenericType(arr);
                        break;
                    case 2:
                        delegateType = typeof(Func<,,>).MakeGenericType(arr);
                        break;
                    case 3:
                        delegateType = typeof(Func<,,,>).MakeGenericType(arr);
                        break;
                    case 4:
                        delegateType = typeof(Func<,,,,>).MakeGenericType(arr);
                        break;
                    case 5:
                        delegateType = typeof(Func<,,,,,>).MakeGenericType(arr);
                        break;
                    case 6:
                        delegateType = typeof(Func<,,,,,,>).MakeGenericType(arr);
                        break;
                    case 7:
                        delegateType = typeof(Func<,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 8:
                        delegateType = typeof(Func<,,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 9:
                        delegateType = typeof(Func<,,,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 10:
                        delegateType = typeof(Func<,,,,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 11:
                        delegateType = typeof(Func<,,,,,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 12:
                        delegateType = typeof(Func<,,,,,,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 13:
                        delegateType = typeof(Func<,,,,,,,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 14:
                        delegateType = typeof(Func<,,,,,,,,,,,,,,>).MakeGenericType(arr);
                        break;
                    case 15:
                        delegateType = typeof(Func<,,,,,,,,,,,,,,,>).MakeGenericType(arr);
                        break;
                    default:
                        delegateType = typeof(Func<,,,,,,,,,,,,,,,,>).MakeGenericType(arr);
                        break;
                }
            }
            return delegateType;

        }
    }

    //被代理类通过类属性标记
    /// <summary>
    /// 具体的某个拦截器 验证是否煮 可以扩展成参数是否正确等
    /// </summary>
    public class Interceptor2 : IInterceptor
    {
        public object Intercept(Invocation invocation)
        {
            Console.WriteLine("煮熟");
            return invocation.Proceed();
        }
    }
    public class Food2
    {
        //类属性
        [Rewrite]
        public virtual string Eat(int p1, int p2)
        {
            return "可以吃了";
        }
    }
    #endregion


    //基于动态例子2的优化

    /// <summary>
    /// 建立 Type和Type的代理类的封装
    /// </summary>
    public class ProxyType<T> where T : IInterceptor, new()
    {
        #region 属性
        public Type Type { get; set; }
        public Type Proxy { get; set; }
        #endregion
        #region 构造函数
        public ProxyType(Type type)
        {
            this.Type = type;
            this.Proxy = ProxyBuilder2<T>.BuildProxy(type);
        }
        #endregion
    }
    /// <summary>
    /// 之前创建类的存放 单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProxyTypeCollection<T> : IEnumerable<ProxyType<T>> where T : IInterceptor, new()
    {
        #region 单例模式
        private static volatile ProxyTypeCollection<T> _instance = null;
        private static readonly object lockHelper = new object();
        //单例双层检查模式
        public static ProxyTypeCollection<T> Instance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ProxyTypeCollection<T>();
                }
            }
            return _instance;
        }
        #endregion
        #region 属性
        private readonly Dictionary<Type, ProxyType<T>> _Items;
        #endregion
        #region 构造函数
        private ProxyTypeCollection()
        {
            _Items = new Dictionary<Type, ProxyType<T>>();
        }
        #endregion
        #region Collection
        public ProxyType<T> this[Type name]
        {
            get
            {
                ProxyType<T> value;
                if (!_Items.TryGetValue(name, out value))
                {
                    value = new ProxyType<T>(name);
                    this.Add(value);
                }
                return value;
            }
        }
        internal void Add(ProxyType<T> value)
        {
            var name = value.Type;
            ProxyType<T> p;
            if (!_Items.TryGetValue(name, out p))
            {
                lock (lockHelper)
                {
                    if (!_Items.TryGetValue(name, out p))
                    {
                        _Items.Add(name, value);
                    }
                }
            }
        }
        public bool ContainsKey(Type name)
        {
            return _Items.ContainsKey(name);
        }
        public ICollection<Type> Names
        {
            get { return _Items.Keys; }
        }
        public int Count
        {
            get { return _Items.Count; }
        }
        #endregion
        #region 迭代器
        public IEnumerator<ProxyType<T>> GetEnumerator()
        {
            foreach (var item in _Items)
            {
                yield return item.Value;
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Items.Values.GetEnumerator();
        }
        #endregion
    }

    /// <summary>
    /// 动态代理的工厂
    /// 说明：
    /// 在第一次需要用到代理时动态生成代理类，之后的使用均调用已经生成的代理类
    /// 规则：
    /// 1.不能为接口创建代理类代理类
    /// 2.父级必须有空的构造函数
    /// 3.只重写virtual的实例方法
    /// 使用说明：
    /// 1.通过CreateInstance生成代理类
    /// 2.通过自定义IInterceptor的实现来自定义代理类对virtual实例方法的重写
    /// </summary>
    public class ProxyFactory<T> where T : IInterceptor, new()
    {
        public S CreateInstance<S>() where S : class
        {
            return (S)Activator.CreateInstance(ProxyTypeCollection<T>.Instance()[typeof(S)].Proxy);
        }

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(ProxyTypeCollection<T>.Instance()[type].Proxy);
        }
    }
}
