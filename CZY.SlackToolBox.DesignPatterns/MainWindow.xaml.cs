using CZY.DesignPatterns.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.DesignPatterns
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            string tempstr = @"实验加载-实验场景-1-2022年2月11日 16:38:39
判断题目-抽取题目-68＋50 = 118-2022年2月11日 16:38:44
实验加载-实验场景-2-2022年2月11日 16:38:46
判断题目-抽取题目-79－78 = 1-2022年2月11日 16:38:50
实验加载-实验场景-3-2022年2月11日 16:38:55
实验加载-实验场景-4-2022年2月11日 16:38:59
是否干预-干预--2022年2月11日 16:39:04
判断题目-抽取题目-92－76 = 16-2022年2月11日 16:39:04
干预方式-干预-前机干预前机|后飞机:干预后机-2022年2月11日 16:39:04
判断题目-超时判断-79－78 = 1-2022年2月11日 16:39:09
判断题目-抽取题目-79－78 = 1-2022年2月11日 16:39:09
";
            string[] msgArr = tempstr.Split('-');
            string id = "";
            bool expflag = false;
            foreach (var item in msgArr)
            {

                if (expflag)
                {
                    expflag = false;
                    id = item;
                }

                if (item == "实验场景")
                {
                    expflag = true;
                }
            }
            MessageBox.Show(id);

            #region 单例模式
            #region 单例饿汉式（静态常量）
            //Singleton.Singleton1 singleton1 = Singleton.Singleton1.getInstance();
            //Singleton.Singleton1 singleton2 = Singleton.Singleton1.getInstance();
            //if (singleton1 == singleton2)
            //{
            //    MessageBox.Show("相同");
            //}
            //if (singleton1.GetHashCode() == singleton2.GetHashCode())
            //{
            //    MessageBox.Show("相同");
            //} 
            #endregion

            #region 单例饿汉式（静态代码块）
            //Singleton.Singleton2 singleton1 = Singleton.Singleton2.getInstance();
            //Singleton.Singleton2 singleton2 = Singleton.Singleton2.getInstance();
            //if (singleton1 == singleton2)
            //{
            //    MessageBox.Show("相同");
            //}
            //if (singleton1.GetHashCode() == singleton2.GetHashCode())
            //{
            //    MessageBox.Show("相同");
            //} 
            #endregion

            #region 懒汉式（线程不安全）
            //for (int i = 0; i < 30; i++)
            //{
            //    ThreadStart threadStart = new ThreadStart(() =>
            //    {
            //        Singleton.Singleton3 singleton1 = Singleton.Singleton3.getInstance();
            //        Singleton.Singleton3 singleton2 = Singleton.Singleton3.getInstance();
            //        if (singleton1 == singleton2)
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //        if (singleton1.GetHashCode() == singleton2.GetHashCode())
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //    });
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //}
            #endregion

            #region 懒汉式（线程安全，同步方法）
            //for (int i = 0; i < 30; i++)
            //{
            //    ThreadStart threadStart = new ThreadStart(() =>
            //    {
            //        Singleton.Singleton4 singleton1 = Singleton.Singleton4.getInstance();
            //        Singleton.Singleton4 singleton2 = Singleton.Singleton4.getInstance();
            //        if (singleton1 == singleton2)
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //        if (singleton1.GetHashCode() == singleton2.GetHashCode())
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //    });
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //}
            #endregion

            #region 懒汉式（线程安全，同步代码块）
            //for (int i = 0; i < 30; i++)
            //{
            //    ThreadStart threadStart = new ThreadStart(() =>
            //    {
            //        Singleton.Singleton5 singleton1 = Singleton.Singleton5.getInstance();
            //        Singleton.Singleton5 singleton2 = Singleton.Singleton5.getInstance();
            //        if (singleton1 == singleton2)
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //        if (singleton1.GetHashCode() == singleton2.GetHashCode())
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //    });
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //}
            #endregion

            #region 双重检查
            //for (int i = 0; i < 30; i++)
            //{
            //    ThreadStart threadStart = new ThreadStart(() =>
            //    {
            //        Singleton.Singleton6 singleton1 = Singleton.Singleton6.getInstance();
            //        Singleton.Singleton6 singleton2 = Singleton.Singleton6.getInstance();
            //        if (singleton1 == singleton2)
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //        if (singleton1.GetHashCode() == singleton2.GetHashCode())
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //    });
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //}
            #endregion

            #region 静态内部类
            //for (int i = 0; i < 30; i++)
            //{
            //    ThreadStart threadStart = new ThreadStart(() =>
            //    {
            //        Singleton.Singleton7 singleton1 = Singleton.Singleton7.getInstance();
            //        Singleton.Singleton7 singleton2 = Singleton.Singleton7.getInstance();
            //        if (singleton1 == singleton2)
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //        if (singleton1.GetHashCode() == singleton2.GetHashCode())
            //        {
            //            MessageBox.Show("相同");
            //        }
            //        else
            //        {
            //            MessageBox.Show("不相同");
            //        }
            //    });
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //}
            #endregion

            //单例模式一共有8种
            #endregion

            #region 工厂模式
            //传统模式是直接对对象实例化，这样一旦对象变化或者同类对象增加，在后续的时候还需要在创建，那之前的所有地方都需要在单独处理一下，
            //使用工厂模式后，提供了统一的实例化方法，在调用的时候统一使用该实例化方法，后续对象变化或者同类对象增加 只要修改工厂，不需要修改其他的地方。
            //代码体现：不直接new对象，而是通过统一的方法去new对象。

            //简单工厂，提供统一的创建对象实例方法，全局都通过这里调用在后期维护的时候就只要维护工厂创建不用全局是维护，方便后期维护使用，优势参考开闭原则
            //FactoryOrder factory = new FactoryOrder();
            //string str = factory.FactorySimpleOrder("Cheese");
            //MessageBox.Show(str);

            //工厂方法， 工厂里面在嵌入工厂的方式，简单方法的进阶。这每次增加的时候就只要维护关联的工厂就可以，不用维护全的工厂。
            //核心思想：基于已有的工厂模式，创建一个类的抽象方法，由抽象的子类去实现其创建方法。这样工厂模式将对象的实例化就放到了子类，这样扩展性更高、维护性更好
            //FactoryOrder factory = new FactoryOrder();
            //string str = factory.FactoryFunOrder("CHINA", "Cheese");
            //MessageBox.Show(str);

            //抽象工厂，提供统一的调用工厂的入口，通过传入不同的独立工厂，确认能够生产及返回的实例。
            //基于已有的工厂模式，将工厂抽象成了两层，一层是抽象工厂，一层是工厂子类，使用时可以根据创建的对象类型使用工厂子类。这样就把简单工厂变成工厂族群，扩展性更高、维护性更好。
            //string str = new FactoryOrder(new AbstractUSAFactory("Chicken")).msg;
            //MessageBox.Show(str);
            #endregion

            #region 原型模式
            //使用场景，需要大量复制对象的时候
            //传统模式
            //在创建对象时总是要获取原来对象的属性，如果创建的对象比较复杂，效率低下
            //在创建时需要重新初始化对象，而不是动态获取，不够灵活，如果第一个变了其他的不跟着变
            //Sheep s = new Sheep("Tom", "21", "白色");
            //Sheep s1 = new Sheep(s.Name, s.Age, s.Color);
            //Sheep s2 = new Sheep(s.Name, s.Age, s.Color);
            //Sheep s3 = new Sheep(s.Name, s.Age, s.Color);
            //Sheep s4 = new Sheep(s.Name, s.Age, s.Color);
            //Sheep s5 = new Sheep(s.Name, s.Age, s.Color);
            //Sheep s6 = new Sheep(s.Name, s.Age, s.Color);
            //MessageBox.Show(s1.Name);
            //MessageBox.Show(s2.Name);
            //MessageBox.Show(s3.Name);
            //MessageBox.Show(s4.Name);
            //MessageBox.Show(s5.Name);
            //MessageBox.Show(s6.Name);

            //原型模式
            //优点：隐藏了创建实例的繁琐过程，只需通过Clone方法就可以获取实例对象
            //使用浅拷贝替代new，减少资源消耗
            //缺点：
            //需要添加一个Clone方法，C#中一般使用MemberwishClone方法来获取实例的浅拷贝副本。
            //补充：如果想实现深拷贝常用的有两种方法：
            //①将原始实例序列化，然后反序列化赋值给副本对象；
            //②浅拷贝 + 递归的方式，类似于遍历文件夹，对所有的复杂属性、复杂属性内部的复杂属性都进行浅拷贝。
            //Sheep s = new Sheep("Tom", "21", "白色");
            //Sheep s1 = (Sheep)s.clone();
            //Sheep s2 = (Sheep)s.clone();
            //Sheep s3 = (Sheep)s.clone();
            //Sheep s4 = (Sheep)s.clone();
            //Sheep s5 = (Sheep)s.clone();

            ////可以看出如果类里面是引用类型 修改之前创建的后续也会有变化
            //Sheep s6 = (Sheep)s.clone();
            //s1.addr.AreaName = "天津";
            //MessageBox.Show(s6.addr.AreaName);

            ////深拷贝
            //Sheep s7 = (Sheep)s.cloneCopy();
            //s1.addr.AreaName = "北京";
            ////输出天津
            //MessageBox.Show(s7.addr.AreaName);
            ////之前的会输出北京
            //MessageBox.Show(s6.addr.AreaName);
            #endregion

            #region 建造者模式

            //正常的模式
            //Building b = new Building();
            //MessageBox.Show(b.BulidHose());


            //建造者模式
            //优点：将产品本身与产品的创建过程解耦
            //将一个复杂对象的构建与它的表示分离，使的同样的构建过程可以创建不同的表示。
            //建造者模式的本质是使组装过程（用指挥者类进行封装，从而达到解耦的目的）和创建具体产品解耦,使我们不用去关心每个组件是如何组装的
            // Building b1 = new Building();
            //MessageBox.Show(b.BuliderHoseStr());
            #endregion

            #region 适配器模式
            //使用场景 当一个类的方法参数不够 去调用第二个方法，  所以需要写一个第三个方法，第三个方法中用来组织调用的参数。分别调用第一个或者第二个。
            //接口不匹配，通过一个方法让其可以使用。一个类的接口转换成另外一个接口，让原本不兼容的类可以兼容。从使用的角度看，第一个类看不到第二个类，只跟适配器类交互。

            //实现
            //adapter类继承 src类 实现  dst接口
            //类适配器  使用继承的方式实现，src类的方法在adapter类中会暴露出来。由于是继承方式可以重写src类。使得adapter更灵活
            //IPowerHole PowerHoleA = new PowerAdapterA();
            //MessageBox.Show(PowerHoleA.Request150V());
            //对象适配器
            //与类适配器是同一种思想，实现方式不同。解决了类适配器必须继承src类的局限性，dst 也可以不用是接口，成本低，更灵活
            //使用合成复用原则优化过来的方式。第七合成复用：在系统中尽量使用关联关系来代替继承关系。 依赖大于继承
            //IPowerHole PowerHoleB = new PowerAdapterB(new PowerV());
            //MessageBox.Show(PowerHoleB.Request5V());
            //接口适配器（缺省适配器）
            //优化对象适配接口，思路：先设计一个抽象类实现接口，并未接口实现默认方法，再由抽象的子类可以选择性的覆盖抽象类的某些方法。
            //使用与dst接口不想使用其所有方法。
            //IPowerHole PowerHoleC = new PowerAdapterC(new PowerV());
            //MessageBox.Show(PowerHoleC.Request150V());
            //MessageBox.Show(PowerHoleC.Request200V());

            #endregion

            #region 桥接模式
            //手机模式  结构型设计模式
            //让程序具有更好的扩展性
            //使用场景：
            //当系统实现有多个角度分类，每种分类都有可能变化使用的。 
            //微服务概念采用了桥接模式的思想，通过各种服务组合来实现一个大的系统。
            //优点：
            //实现抽象与具体分离，降低了各个分类角度间的耦合
            //扩展性好，解决多角度分类使用继承可能会出现的子类爆炸的问题
            //缺点：
            //桥接模式的引进需要通过聚合关联建立抽象层，增加理解和设计系统的难度
            //IBrand brand = new HW();
            //Phone phone = new FoldedPhone();
            //phone.SetBrand(brand);
            //MessageBox.Show(phone.Open());
            //MessageBox.Show(phone.Call());
            //MessageBox.Show(phone.Close());
            //phone.SetBrand(new IPhone());
            //MessageBox.Show(phone.Open());
            //MessageBox.Show(phone.Call());
            //MessageBox.Show(phone.Close());
            #endregion

            #region 装饰者模式
            //动态的将功能附加到对象上，在对象功能扩展方面，它比基础更有弹性，装饰者也体现了开闭原则。
            //Drink order = new LongBlackCoffee();
            //info.Content = "水单费用= " + order.Cost() + "\r\n";
            //info.Content += "订单详情= " + order.Des + "\r\n";

            //order = new Milk(order);
            //info.Content += "订单 加入了= " + order.Detail + "\r\n";
            //info.Content += "水单费用 = " + order.Cost() + "\r\n";


            //order = new Chocolate(order);
            //info.Content += "订单 加入了= " + order.Detail + "\r\n";
            //info.Content += "水单费用 = " + order.Cost() + "\r\n";

            //order = new Chocolate(order);
            //info.Content += "订单 加入了= " + order.Detail + "\r\n";
            //info.Content += "水单费用= " + order.Cost() + "\r\n";

            #endregion

            #region 组合模式
            // Component    这是组合中对象声明的接口，在适当的情况下，实现所有类共有的接口默认行为，
            //              用于访问和管理Component子部件。Component可以是抽象类也可以是接口
            // Leaf         在组合中表示叶子节点，叶子节点没有子节点
            // Composite    非叶子节点，用于存储子部件，在Component接口中实现子部件的相关操作。例如：Add Del Edit...

            // 说明：将对象组合成树形结构以表示“部分-整体”的层次结构，用户对单个对象和组合对象的使用具有一致性
            // 所以当我们的需求是树形结构或者是部分-整体的关系时，就可以考虑使用组合模式。
            // 组合模式有两种不同的实现，分别为透明模式和安全模式
            // 希望用户忽略组合对象与单个对象的不同，用户将统一的使用组合结构中的所有对象

            //使用组合模式时，其叶子和树枝的声明都是实现类，而不是接口，违反了依赖倒转原则

            //创建学校
            //SchoolComponent component = new University("清华大学", "中国第一大学");

            //创建学院
            //SchoolComponent computerCollege = new College("计算机学院", "------------------");
            //SchoolComponent infoCollege = new College("信息工程学院", "------------------");
            //SchoolComponent builderCollege = new College("土木工程学院", "------------------");

            //computerCollege.Add(new Department("软件工程", "软件工程不错"));
            //computerCollege.Add(new Department("网络工程", "网络工程不错"));
            //computerCollege.Add(new Department("计算机科学与技术", "计算机科学与技术是老牌的专业"));

            //infoCollege.Add(new Department("通信工程", "通信工程 不好学"));
            //infoCollege.Add(new Department("信息工程", "信息工程 很好学"));

            //builderCollege.Add(new Department("测绘", "测绘 不好学"));

            //component.Add(computerCollege);
            //component.Add(infoCollege);
            //component.Add(builderCollege);

            //info.Content = component.Display();

            #endregion

            #region 外观模式
            //外观类 （Facade）:为调用端提供统一的接口
            //调用者 （client）:外观类的调用方
            //子系统集合       :指模块或者子系统，处理Facade指定的任务。是功能的具体实现

            //外观模式 对外屏蔽了子系统或者子模块的细节，降低了客户端对子系统使用的复杂性。
            //外观模式 对客户端与子系统的耦合关系解耦，让子系统更容易扩展。
            //通过合理的使用外观模式，可以帮我们更好的划分访问层次。
            //注意 不是为了用而用，而是当系统复杂度后维护难度，扩展难度，这个模式是以梳理系统层次结构为主。
            //SysFacade sys = new SysFacade();
            //info.Content += sys.Ready();
            //info.Content += "系统开机完成\r\n";
            //info.Content += sys.UpFloor();
            //info.Content += "上升楼层中\r\n";
            //info.Content += sys.ArriveFloor();
            //info.Content += "到达楼层\r\n";
            //info.Content += sys.DownFloor();
            //info.Content += "下降楼层\r\n";
            //info.Content += sys.ArriveFloor();
            //info.Content += "到达楼层\r\n";
            //info.Content += sys.Close();
            //info.Content += "关闭系统\r\n";


            #endregion

            #region 享元模式
            //FlyWeight 是抽象的享元角色，他是产品的抽象类，同时定义出对象的外部、内部状态的接口或实现
            //ConcreteFlyWeight  是具体的享元角色，是具体的产品类，实现抽象角色定义相关业务。
            //UnSharedConcreteFlyWeight 是不可共享的模式，一般不会出现在享元工厂
            //FlyWeightFactory 享元工厂。用于构建一个池容器，同时提供从池中获取对象方法。

            //享元模式 必须区分出类的内部状态与外部状态
            //内部状态：指对象共享出来的信息，存储在享元对象内部但不会随着环境改变而改变
            //外部状态：指对象得以依赖的一个标记，是随着环境改变而改变、不可共享状态
            //五子棋  坐标是外部状态  颜色是内部状态相对少固定。

            //优点
            //能够减少对象的开销。

            //缺点 
            //增加了耦合

            //WebsiteFactory web = new WebsiteFactory();
            ////发布新闻网站
            //Website website1 = web.getWebsiteConcrete("新闻");
            //info.Content += website1.use(new User("陈志羽")) + "\r\n";
            ////发布新闻博客
            //Website website2 = web.getWebsiteConcrete("博客");
            //info.Content += website2.use(new User("杨升")) + "\r\n";

            ////发布新闻博客
            //Website website3 = web.getWebsiteConcrete("博客");
            //info.Content += website3.use(new User("刘文斌")) + "\r\n";

            ////发布新闻博客
            //Website website4 = web.getWebsiteConcrete("博客");
            //info.Content += website4.use(new User("詹翠生")) + "\r\n";


            //info.Content +=   "网站的分类共有 "+ web.getWebsiteCount()+ "  个\r\n";
            #endregion

            #region 代理模式
            //为一个对象提供一个替身，以控制对这个对象的访问，即通过代理对象访问目标对象，
            //这样做的好处是，可以在目标对象实现的基础上，增强额外的功能操作即扩展目标对象的功能

            //被代理的对象可以是 远程对象、安全控制对象、开销大的对象

            //代理模式分为三种 静态代理、动态代理、Cglib代理

            //网上解释：
            //当无法直接访问某个对象或访问某个对象存在困难时可以通过一个代理对象来间接访问，
            //为了保证客户端使用的透明性，所访问的真实对象与代理对象需要实现相同的接口。根据代理模式的使用目的不同，代理模式又可以分为多种类型，
            //例如保护代理、远程代理、虚拟代理、缓冲代理等，它们应用于不同的场合，满足用户的不同需求。

            //优点：在不修改目标对象的功能前提下，能通过代理对象对目标功能扩展。
            //缺点：因为代理对象需要与目标对象一起实现一样的接口，所以会有很多代理类，一旦代理的接口增加方法，目标对象与代理对象都需要维护。

            ////创建被代理对象
            //TeacherDao teacher = new TeacherDao();
            ////创建代理对象
            //TeacherDaoProxy teacherProxy = new TeacherDaoProxy(teacher);
            ////执行的是代理对象的方法，代理对象中在去调用目标对象中的方法
            //info.Content = teacherProxy.teach();



            ////动态代理，反射机制 网上例子1 
            ////思路说明:
            ////根据被代理对象获取代理对象类型
            //Type tf = ProxyBuilder.BuildProxy(typeof(Food));
            ////Activator.CreateInstance C# 根据类型动态创建对象方法
            ////动态创建代理对象，并转换成被代理对象。
            //Food f = (Food)Activator.CreateInstance(tf);
            ////由于创建 的对象原来是代理对象，转换的代理对象。
            ////保留其原来作为子类的特性。
            ////所以先执行的是代理类的Eat，在Eat中在人为的调用父类的Eat方法。
            //info.Content= f.Eat(1, 2);


            //动态代理。优化方式1
            //根据编译器会把源代码（C#）编译成托管代码（IL），托管代码在公共语言运行库(CLR)中运行。
            //当方法被调用时，再通过即时编译器（JIT）编译成机器码。
            //而Emit的作用可以理解为直接操作托管代码来生成类或者方法。
            //由此思路扩展 根据被代理对象获取代理对象类型
            //Type tf = ProxyBuilder2<Interceptor2>.BuildProxy(typeof(Food2));
            //Food2 f2 = (Food2)Activator.CreateInstance(tf);
            //info.Content = f2.Eat(1, 2);

            //动态代理。优化方式2 
            //实现优化方式1的时候由于每次使用都是 泛型动态创建，每次创建开销大，程序运行速度慢。
            //优化的方式为，创建一个单例字典，在通过工厂模式封装。在每次创建完成后就放入字典中，
            //如果下次用的时候就直接用，不要用在去创建了
            //如此一来，使用知道到FoodProxy的存在，而实际生效的却是FoodProxy。

            //Food2 f3 = new ProxyFactory<Interceptor2>().CreateInstance<Food2>();
            //info.Content = f3.Eat(1, 2);
            #endregion

            #region 模板模式
            //制作豆浆、先选材料->浸泡->放到豆浆机中
            //添加不同豆浆材料制作不同口味的豆浆

            //代码实现
            //在一个抽象类公开定义了执行它的方法的模板，它的子类可以按照需要重写方法实现，但调用将以抽象类中定义的方法进行
            //示例说明
            //定义一个操作中的算法流程骨架，将一些步骤延迟到子类中，使得子类可以不改变一个算法流程结构，就可以重定义该算法的某些特定步骤。
            //这种类型属于行为模式

            //制作黄豆豆浆
            //MakeSoyMilk soy = new Soybean();
            //info.Content=soy.Make();
            ////制作花生豆浆修改器机器的使用
            //MakeSoyMilk soyMilk = new Peanut();
            //info.Content += soyMilk.Make();
            ////制作纯豆浆，不更换材料    钩子方法概念，让其子类看情况是否使用这个步骤 变成新的流程
            //MakeSoyMilk pure = new Pure();
            //info.Content += pure.Make();
            #endregion

            #region 命令模式
            //使用场景
            //向不知道的对象发送请求， 不知道请求的接收者。也不知道被请求的操作。且命令是可撤销的。
            //例子：
            //一个将军向士兵发送命令，士兵去执行。拆解角色：将军（命令发布者）、士兵（命令具体执行者）、命令（链接将军和士兵）
            //Invoker 调用者 Receiver 被调用者 MyCommand 命令 其中MyCommand 实现Command接口，持有接收对象。
            //编写模式的例子：
            //万能遥控器，控制电灯、冰箱、空调、洗衣机、电饭煲。
            //思路：
            //编写遥控器类，在遥控器中设置遥控器按钮，给每个按钮绑定命令
            //编写命令接口，遥控器只绑定命令接口，不管命令是由谁实现的
            //编写命令实现（被调用者）实现命令控制的逻辑，实现命令接口，在命令接口中写对应的控制逻辑，例如：控制电灯、电饭煲的逻辑
            //编写电灯、电饭煲(操作对象)。电灯、电饭煲具体的功能实现。

            //使用原则：开闭原则
            //命令模式的优点：
            //将发起请求的对象与执行请求的对象解耦。
            //容易实现撤销和重做。

            //缺点
            //系统大的情况下会导致系统有过多的具体命令类。
            //增加系统复杂度。

            //经典案例：界面上一个按钮就是一个命令，模拟cmd订单的撤销/恢复、触发反馈机制。

            //电灯实际操作
            //LightReceiver lightReceiver = new LightReceiver();
            ////电饭煲
            //RiceCookerReceiver riceCookerReceiver = new RiceCookerReceiver();

            ////按钮按下一次
            //LightCommand lightOn = new LightCommand();
            //info.Content += lightOn.SetBinding(lightReceiver);
            ////按钮按下第二次
            //LightCommand lightOff = new LightCommand();
            //info.Content += lightOff.SetBinding(lightReceiver);

            ////给遥控器按钮设置命令
            //RemoteControl remoteControl = new RemoteControl();
            //remoteControl.setCommand(0, lightOn, lightOff);
            //info.Content += remoteControl.onButtonCommand(0, "001");
            //info.Content += remoteControl.onButtonCommand(0, "002");
            //info.Content += remoteControl.undoButtonCommand("001");



            ////电饭煲按钮按下一次
            //RiceCookerCommand RiceCookerOn = new RiceCookerCommand();
            //info.Content += RiceCookerOn.SetBinding(riceCookerReceiver);
            ////电饭煲按钮按下第二次
            //RiceCookerCommand RiceCookerOff = new RiceCookerCommand();
            //info.Content += RiceCookerOff.SetBinding(riceCookerReceiver);
            ////将命令绑定到遥控器上
            //remoteControl.setCommand(1, RiceCookerOn, RiceCookerOff);
            //info.Content += remoteControl.onButtonCommand(1, "001");
            //info.Content += remoteControl.onButtonCommand(1, "00201");
            //info.Content += remoteControl.onButtonCommand(1, "00202");
            //info.Content += remoteControl.undoButtonCommand("001");

            #endregion

            #region 访问者模式
            //作用:将数据结构与数据操作分离。解决数据结构和操作耦合性问题
            //工作原理：在被访问的类里面加一个对外提供接待访问者的接口。
            //应用场景：需要对一个对象结构中的对象进行很多不同的操作（这些操作彼此之间没有关联），同时要避免让这些操作”污染“对象类。

            //Visitors 是抽象访问者，为该对象结构中的concreteElement的每一个类申明一个Visit操作。
            //ConcreteVisitor 是具体的访问值，实现每个Visitors申明的操作，每个操作实现的部分。
            //ObjectStructure  是枚举元素、可以提供一个高层接口用来允许访问者访问发的元素。
            //Element 是定义了一个Accept方法，接收一个访问者对象。
            //concreteElement 具体的元素，实现Accept方法。



            //双分派：
            //不管类怎么变化，我们都能找到期望的方法，双分派的意义是得到执行得到操作取决于种类和两个接收者的类型

            //优点
            //访问者模式符合单一职责原则、让程序具有优秀的扩展性、灵活性非常高
            //可以对功能进行统一、可以做报表、UI使用与数据结构稳定的系统

            //缺点
            //具体元素对访问者公布细节，违背迪米特法则
            //违背了依赖倒置原则。高层模块与底层模块直接不应该直接依赖，而应该依赖抽象


            //例子：某个系统给男人女人评分。
            //ObjectStructure objectStructure = new ObjectStructure();
            //objectStructure.Add(new Man());
            //objectStructure.Add(new Woman());

            //Good g = new Good();
            //info.Content+= objectStructure.Display(g);

            //Pass p = new Pass();
            //info.Content += objectStructure.Display(p);


            #endregion

            #region 迭代器模式
            //行为设计模式
            //提供一种遍历集合的统一接口，用一致的方法遍历集合元素，不需要知道集合对象底层表示，不暴露内部结构
            //迭代器模式承担了遍历集合对象的职责，则该模式自然存在2个类，一个是聚合类，一个是迭代器类(通用遍历类)。
            //在面向对象涉及原则中还有一条是针对接口编程，所以，在迭代器模式中，抽象了2个接口，一个是聚合接口，另一个是迭代器接口，这样迭代器模式中就四个角色了
            IteratorAbstract iterator;
            IListCollection list = new ConcreteList();
            iterator = list.GetIterator();

            while (iterator.MoveNext())
            {
                int i = (int)iterator.GetCurrent();
                MessageBox.Show(i.ToString());
                iterator.Next();
            }

            #endregion
        }
    }
}
