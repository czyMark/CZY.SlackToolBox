using System;
using System.Collections.Generic;

namespace CZY.SlackToolBox.FastExtend
{

    public class RandomData
    {
        /// <summary>
        /// 随机数
        /// </summary>

        private static RandomData instance;
        public static RandomData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RandomData();
                }
                return instance;
            }
        }



        /// <summary>
        /// 世界货币单位
        /// </summary>
        public Dictionary<string, string> MoneyUnit;


        /// <summary>
        /// 轮船交通工具
        /// </summary>
        public string[] Ships;
        /// <summary>
        /// 飞行交通工具
        /// </summary>
        public string[] AirPlanes;
        /// <summary>
        /// 陆地交通工具
        /// </summary>
        public string[] Transportations;


        /// <summary>
        /// 公司涉及的单据
        /// </summary>
        public string[] Receipts;

        /// <summary>
        /// 公司各个部门名称
        /// </summary>
        public string[] DepertmentNames;

        /// <summary>
        /// 百家姓中的单姓
        /// </summary>
        public string[] SingleNames;

        /// <summary>
        /// 百家姓中的复姓
        /// </summary>
        public string[] DoubleNames;

        /// <summary>
        /// 百家姓中的所有姓氏
        /// </summary>
        public string[] AllNames;

        /// <summary>
        /// 常见名
        /// </summary>
        public string[] Names;

        /// <summary>
        /// 电信手机号开头
        /// </summary>
        public string[] CTC;


        /// <summary>
        /// 联通手机号开头
        /// </summary>
        public string[] CUCC;


        /// <summary>
        /// 移动手机号开头
        /// </summary>
        public string[] CMCC;


        /// <summary>
        /// 所有地区
        /// </summary>
        public string[] Region;
        /// <summary>
        /// 省
        /// </summary>
        public Dictionary<string, string> Province = new Dictionary<string, string>();
        /// <summary>
        /// 省对应的简称
        /// </summary>
        public Dictionary<string, string> ProvinceNative = new Dictionary<string, string>();
        /// <summary>
        /// 城市
        /// </summary>
        public Dictionary<string, string> City = new Dictionary<string, string>();

        /// <summary>
        /// 邮箱
        /// </summary>
        public string[] EMail;


        /// <summary>
        /// 行政编号
        /// </summary>
        public string[] RegionCode;
        /// <summary>
        /// 职位
        /// </summary>
        public string[] ITPosition;

        /// <summary>
        /// 常见成语
        /// </summary>
        public string[] Idioms;


        /// <summary>
        /// 城市机构
        /// </summary>
        public string[] CityInstitution;

        /// <summary>
        /// 中央部门机构
        /// </summary>
        public string[] CentralGovInstitution;

        private RandomData()
        {
            #region 姓名

            string temp = "赵,钱,孙,李,周,吴,郑,王,冯,陈,褚,卫,蒋,沈,韩,杨,朱,秦,尤,许,何,吕,施,张,孔,曹,严,华,金,魏,陶,姜,戚,谢,邹,喻,柏,水,窦,章,云,苏,潘,葛,奚,范,彭,郎,鲁,韦,昌,马,苗,凤,花,方,俞,任,袁,柳,酆,鲍,史,唐,费,廉,岑,薛,雷,贺,倪,汤,滕,殷,罗,毕,郝,邬,安,常,乐,于,时,傅,皮,卞,齐,康,伍,余,元,卜,顾,孟,平,黄,和,穆,萧,尹,姚,邵,湛,汪,祁,毛,禹,狄,米,贝,明,臧,计,伏,成,戴,谈,宋,茅,庞,熊,纪,舒,屈,项,祝,董,梁,杜,阮,蓝,闵,席,季,麻,强,贾,路,娄,危,江,童,颜,郭,梅,盛,林,刁,钟,徐,邱,骆,高,夏,蔡,田,樊,胡,凌,霍,虞,万,支,柯,昝,管,卢,莫,经,房,裘,缪,干,解,应,宗,丁,宣,贲,邓,郁,单,杭,洪,包,诸,左,石,崔,吉,钮,龚,程,嵇,邢,滑,裴,陆,荣,翁,荀,羊,於,惠,甄,曲,家,封,芮,羿,储,靳,汲,邴,糜,松,井,段,富,巫,乌,焦,巴,弓,牧,隗,山,谷,车,侯,宓,蓬,全,郗,班,仰,秋,仲,伊,宫,宁,仇,栾,暴,甘,钭,历,戎,祖,武,符,刘,景,詹,束,龙,叶,幸,司,韶,郜,黎,蓟,溥,印,宿,白,怀,蒲,邰,从,鄂,索,咸,籍,赖,卓,蔺,屠,蒙,池,乔,阳,郁,胥,能,苍,双,闻,莘,党,翟,谭,贡,劳,逄,姬,申,扶,堵,冉,宰,郦,雍,卻,璩,桑,桂,濮,牛,寿,通,边,扈,燕,冀,姓,浦,尚,农,温,别,庄,晏,柴,瞿,阎,充,慕,连,茹,习,宦,艾,鱼,容,向,古,易,慎,戈,廖,庾,终,暨,居,衡,步,都,耿,满,弘,匡,国,文,寇,广,禄,阙,东,欧,殳,沃,利,蔚,越,夔,隆,师,巩,厍,聂,晁,勾,敖,融,冷,訾,辛,阚,那,简,饶,空,曾,毋,沙,乜,养,鞠,须,丰,巢,关,蒯,相,查,后,荆,红,游,竺,权,逮,盍,益,桓,公";
            SingleNames = temp.Split(',');
            temp = "万俟,司马,上官,欧阳,夏侯,诸葛,闻人,东方,赫连,皇甫,尉迟,公羊,澹台,公冶,宗政,濮阳,淳于,单于,太叔,申屠,公孙,仲孙,轩辕,令狐,徐离,宇文,长孙,慕容,司徒,司空";

            DoubleNames = temp.Split(',');
            AllNames = new string[SingleNames.Length + DoubleNames.Length];
            for (int i = 0; i < SingleNames.Length; i++)
            {
                AllNames[i] = SingleNames[i];
            }
            for (int i = 0; i < DoubleNames.Length; i++)
            {
                AllNames[SingleNames.Length + i] = DoubleNames[i];
            }
            temp = "庆雪,启迪,心然,雨霏,涵诺,飞凡,梓炜,惠芬,晓东,晨婷,荺汀,跳跳,仕龙,煥义,裕连,秋明,嵩杰,睿轩,轶群,歆捷,柏晗,楚洋,赫男,名宴,骄阳,连付,芯瑜,楚升,煜菲,瑞驰,傲熙,佳豪,裕峥,飞飞,颡兴,淑轩,文恒,欧楠,羽薇,龙辰,纪言,书德,柳诺,明宏,一轩,润生,旭明,圣楠,博赡,良程,昕晴,楷硕,辰龙,圳矜,冰鑫,清发,延夏,欣汝,振宇,若欣,昌聪,金君,研霓,赞浚,文彦,一轩,金颖,启芮,思彤,洛唏,锦涛,哲極,北峰,朋蕾,馨茹,子雨,征鲤,子彦,晨旭,苈殿,添睿,梓强,晨阳,阮变,春红,谦桐,一帅,嘉畅,宇萱,圣盛,冰瑜,杳篱,毅騉,舣显,昊苍,雅倩,毕友,豪锋,卫锋,春蕾,泓乐,湘渝,紫嫣,浩博,曜儿,一新,嘉明,百漩,碧婷,企志,艺馨,溪桐,力横,鑫源,倩雪,黎钦,益智,静轩,粟琳,子健,觉豪,金娜,先忍,传鸿,鹏涛,雨婷,玉洁,宏博,悠悠,晓萍,幕晞,鸿哲,玲睿,汉宸,铱卉,成诚,雅鑫,智芳,睿林,劳逸,朔骧,鑫研,昕彤,若晗,克军,静文,常路,浩蕊,志朗,至德,倍汶,康年,卜晨,昕梦,新畅,雅彤,炎劬,红叶,武涛,土纠,沛雯,姿岚,斌翼,佩雅,一如,冲牧,子皓,雅俐,工煜,礼鸿,敖恒,希晟,廷昊,绍芹,朋淏,丽晓,响纸,芷婧,红新,到小,谙易,雨笙,言柔,辞舒,嘉璋,丹华,矗朋,利财,雨萌,丽慧,宸泽,瑞杰,传幸,靖琪,跃明,字莟,艺淏,子成,圣尧,明桦,湛淼,浛琳,宸瑞,誉官,泽民,庆南,雅熙,希焱,巾瑞,大芹,妍丁,沁池,奕博,朝仁,韦烜,高昂,振南,佳泽,岩泓,裕喆,俊基,赛赛,嘉旭,硕桢,博涵,郁秦,安弘,启正,佳悦,槐英,裕亮,铭阳,连山,舒雨,仪涵,浩营,佑明,紫仪,弘毅,正康,金凤,联鸿,小粟,宽钊,腾泽,哲侑,嘉濠,金河,瑶瑶,銮天,水淼,晟熙,博翰,景龙,傲君,小迪,晨瑜,年烨,昇睿,霈珺,潇泽,涵羲,燕兰,抒辰,佳谦,仁远,新玲,龙泽,泽晞,艺舫,阳怡,家榕,昞吉,维哲,晟霖,神州,昶蛑,佩华,向东,梓莘,佳雨,睿滔,超凡,恒贵,忻楠,辰海,如源,竣玮,宝艺,炜行,炳丁,河沐,正坤,崞瑙,敖征,清睿,英超,先书,晨峰,子权,曦熙,弘图,希霆,晨訏,锡海,瑞溯,苏涵,朋德,欣瑜,柳宁,艺婷,秋芳,孩伺,铃绳,帏遵,清澈,丽娜,子键,宝萱,和蔼,祥溢,昊洲,启恒,海华,浩熠,晓琳,婉轩,艺涵,继轩,镇海,瀚宇,峻楠,妍艳,睿鑫,鸿煊,润皑,梓炜,惠芬,晓东,晨婷,荺汀,跳跳,雅林,裕涵,伟诚,明轩,毅杨,佟裴,瑾轩,翰一,玥彤,朝华,承眧,梓澧,梓灿,岚玺,博宇,少婷,玥晨,文昭,佳旭,雪晴,兴棒,梓滢,铴均,海超,传钧,涵容,腾腾,鸿远,会茹,睿博,昊雨,金溪,惋钥,澍域,天惠,子耀,远海,缤翌,粟燊,颖潼,屹俊,卓璇,昊旭,贺琳,傲珺,祖瀚,艺华,恩旭,世恒,基钰,又又,益轩,宇昊,梓悦,蔡齐,涵仪,宇昂,泽瑄,梅彤,力蔺,智恒,静轩,雅文,益华,峻贤,志姚,翼程,龙归,一袁,伟德,文千,觉农,春豪,火南,博允,涵悦,晶婧,梓轩,锣使,琪博,烁鸿,圣泽,玺豪,瑞澜,才艺,庆宝,宇卓,成桥,启源,锦绣,靖茹,钰喆,炜茵,思圣,露颖,东想,婵英,宙抑,思颖,鎔洲,远亮,佳彤,子探,佳皓,拥天,子羽,广雷,睿昶,钊锐,皓博,谚熙,佐豪,思淇,博林,秋茵,兆江,腾月,箐苗,雨莺,焕发,卜晨,佳琳,义泽,奇俊,铭瑞,瑞洋,祥博,红梅,冰兰,豪轩,建业,地财,翊桦,冠阅,语瞳,荣昙,轾恒,佳琦,沄欣,朋蔚,晓娟,澄澄,洪九,海露,栢玮,涵育,邦浩,信俞,宗鹏,智勋,文硕,齐佐,依凡,香凝,思远,正帆,祺媛,熔雯,亦煊,晨菥,子缮,延责,传豪,佳芸,依伊,晨诺,韬译,诗轩,娟欣,婷可,芬依,留瑛,培娟,虞婉,童玲,矩文,悦中,濮文,善美,晖娅,莹香,哓婧,梅霎,当玲,叶瑛,三玉,婷茵,燕斜,豫瑞,埠洁,沛源,二萍,彩媛,燮莹,典萍,梓婷,绳琳,璐云,球芳,莹麟,奥芬,彭美,洛伊,宫文,苗莹,郭梅,婷蛎,娟娟,育莹,卉妹,军娟,桐洁,琴陈,莹秀,昱媛,怡兰,尚瑶,玲借,些忆,慧欢,沫婷,爽娜,燕亭,召花,倩夷,陶怡,俏雪,都琴,萤文,保秀,珂莹,憧琳,续裴,序霞,腮蓉,城芳,娟嘉,怡蓝,厉娟,贞瑶,映萱,席萍,秀隰,路花,文婷,艿艳,渌芳,悦合,芬露,晴岚,秀忻,梓瑶,亿婧,蓉端,梦妙,钏尼,箢文,沭蓉,务英,紫若,渊文,柬玲,绘芬,烽丽,婕婵,洁佳,妍蒂,迁文,贯洁,海熙,亚锦,岚洁,薪茹,雪熙,莉溶,根妍,沈怡,德娜,涞莉,曼烟,桀文,媛宜,裔娟,沅娟,燕钢,蓉钻,珞莹,沁雨,臣文,欣芙,桂依,明丽,雯鸣,至怡,瑛裕,汶瑶,旨悦,桠蓉,娟茹,良美,艺茹,蕈辂,伊倩,吟梅,彤秀,沂芬,泓琴,娜轩,淑榆,炳娥,西蓉,余丽,舜瑶,璋妍,荣英,魅倩,婧霞,妮莲,由英,珏悦,佩明,琳倍,玉妹,球英,丽彤,平妹,娟阁,婧攸,苣文,荷怡,栋倩,晓怡,让悦,竹芳,倚岚,甜洁,莹伟,曦颖,銮菀,享英,蓉蔓,粟霞,义莉,朵拼,霍文,文琼,娅沁,写文,钧玉,茂瑛,铭琼,曦丽,嚣妍,非妍,寒怡,倩珑,莉扬,素佳,刘瑛,渝轩,牡芬,冰怡,苓蓓,莉萨,丽琼,静蓉,外芳,昕芬,殷秀,蕊英,秀染,松妍,二媛,憎怡,坷艳,溯娟,昕梦,凝阳,潍婧,燕昭,雪琦,怀霞,珠听,倩曼,腮蓉,城芳,娟嘉,怡蓝,厉娟,贞瑶,滢文,平丽,岫婷,茕文,愉琴,皙萍,媛迤,申妞,邺琳,琳琦,熙雯,沐妮,捷莹,凰玉,萱琳,鑫嫣,秀熹,芳宛,格颖,灏玉,雍丽,其妹,蝉莹,碧霓,厚美,瑞贞,旦怡,潞梅,梦娅,及玉,希瑶,馗文,宣英,琳馥,静美,咚丽,菱荔,暄茹,兆丽,犹美,采丽,柔雪,仨艳,雍霞,倩芝,鹰倩,艳玲,博娅,秋嘉,秀霆,蜻芳,寅艳,琴妙,添娥,琳羽,玢琳,云萍,若禧,妍宏,祺婷,愈红,裔莹,茹加,炫娜,熔倩,金凤,芳榆,鸾玉,宇红,上美,腊媛,婧揉,瑶彡,素钰,宫瑛,悔霞,馨芳,蒙燕,利莉,婷毓,莹郦,泾瑛,芹雪,化娥,秋花,淞动,莹四,稚娅,穗雪,映娟,生雪,里英,娅儇,岈娟,霞冰,长颖,陈娜,莘琴,孜梅,茹纬,钱媛,珠荣,郁娟,妍桦,缘桑,娅忻,婕丽,洋丽,聍文,娟镅,琳特,赴霞,任千,桓琼,柿红,明佳,雯嘉,赛瑛,璐芝,中郡,念桃,钰佳,欣昕,菏娟,竣倩,闰梅,诩艳,舂婧,若婷,昱艳,裕琳,俞君,昱茹,镱颖,晶馨,晋颖,观玉,花萍,茹镱,琳婷,诗楠,仔婧,瑛立,霞俊,旖舷,郜妍,召芬,觯颖,笙悦,娅叙,丛芸,琳皖,芬远,单怡,捷萍,萁悦,悦枚,玲峥,温媛,谦蓉,琳娉,博琴,媛袁,见梅,珺云,彗芳,尹茹,菱悦,辉玉,荔琴,远琴,秀圆,祉妍,茹敬,颀倩,侠蓉,妍忠,飞婵,同玉,漫怡,沃蓉,绘妍,珊倩,婧影,曼钰,敏琳,广冉,冰玥,品梅,芷嫣,弼莹,炀洁,百妍,纱莉,玲彦,琳凇,倩纭,彤少,励花,瑶藜,蔷琳,瑰花,韵艳,修花,琳鳞,圆洁,晓语,娅惟,夜芬,悦沐,锦涵,霞颖,枫洁,荷悦,蓣婷,怡人,诚倩,玲玲,润玉,汀婵,锵萍,宵莹,怡佾,蕊悦,楼瑛,旦萍,麟娥,一妍,钦蓉,芳冰,伽美,私蓉,瑛帅,关艳,波蓉,忆梅,祯妍,萍华,润幽,雩瑶,诩艳,舂婧,若婷,昱艳,裕琳,俞君,音美,妍焰,煦婵,显燕,议茹,闻琳,味芳,悦萱,卉燕,诗雪,琳牧,莉媚,育梅,婧宜,骏文,映莉,慈梅,芳瑾,昌文,琳诗,弋玉,镁莉,漫瑶,慈颖,馥霞,妮黛,几莉,潍美,诘冉,玲淋,雨妙,媛如,妍嬉,莹飞,倩姣,芷巧,北燕,琳名,琳玟,梦璐,以菱,炀丽,伦玉,钰雅,欣瑶,坤文,欲蓉,朵雯,高梅,浠颖,寒瑜,媛郴,熙莹,莹锌,岘婷,欢霜,梨莉,沛妤,砚琼,悦祖,琳硕,治玉,嗳琴,芬清,铭英,阮婷,曦悦,悦枷,婵葶,小梅,幸艳,刚琴,逸娟,用玲,泯燕,麒琳,婧祺,俸颖,雪玲,长雪,振玉,璀瑶,抒美,莉湄,祥琳,釜茹,潆妍,缌瑶,艳莉,弘玲,嫣阳,欣青,云嫦,嫣阑,恺婷,雯菲,英诗,婷硕,书琼,璐莹,桦媛,协娥,岂文,怡娟,尤琼,倚芬,琦淑,卓婷,亭蓉,夷琼,寄瑶,睦萍,艳嫣,悦仟,小红,凯妞,伦颖,琳钥,帛文,璐蓉,秀玑,原莉,莫琳,仪莹,冉娜,乔英,甜绿,泓莹,倩同,陟文,氏艳,佳恬,义机,妁悦,茵曦,研红,绎洁,布梅,紫娇,欣润,琳彤,习霞,伊琴,营雪,森,泰,信,冉,宏,弛,婷,恺,中,杉,洋,敏,治,桃,北,娟,勋,润,壹,山,宓,余,暄,欢,曦,新,初,凤,弘,月,沁,承,亮,微,尘,含,波,樱,哲,槿,媛,启,湙,伟,丞,扬,淇,岑,彬,丽,江,晴,七,岚,昊,渤,希,宥,正,庚,泉,喆,永,影,尧,搏,本,亭,善,德,云,昆,和,嫒,文,心,杰,强,昭,楸,曈,志,晨,书,宇,橙,坤,松,懿,乐,夏,子,娴,争,俊,枫,墉,天,妙,彭,妍,晓,悦,晶,可,榕,淼,慈,亿,妤,沐,园,宸,嫣,忆,泽,一,傲,棋,杭,沂,圆,柔,权,伶,名,义,川,伦,彰,岛,昂,桐,彦,嫚,朴,婴,娅,兮,汀,墨,婧,易,沫,浩,帆,柏,佑,君,映,容,城,意,偲,刚,晗,愉,望,丹,楷,宵,才,东,清,斯,梦,星,湖,昕,斌,晔,恩,允,勇,武,棣,坷,成,儒,予,恒,歌,桦,倩,林,婉,卿,栩,民,汐,征,汜,楠,梓,国,姝,廷,栋,冰,严,洁,泓,任,佳,宜,兴,彤,多,彧,少,宁,振,旗,依,于,晋,桁,华,嵩,晟,明,奕,杨,朵,景,毅,元,惠,旭,春,仪,淳,宝,夕,槊,旺,商,歆,梵,果,仁,沣,旻,浠,洛,涵,峰,兰,安,培,格,乾,伊,康,怡,延,卓,妮,政,涛,凯,屹,呈,富,二,如,博,慧,佛,同,儿,匀,奇,斐,之,亚,昱,婕,乔,平,垚,思,欣,凡,渊,庭,娜,洪,嘉,智,怀";
            Names = temp.Split(',');
            #endregion

            #region 组织架构
            temp = "总经理,副总经理,高层管理委员会,各执行委员会,总经理办公室,督察预警部,督察工作室,保卫科,行政管理部,行政事务科,党委办公室,工会办公室,档案管理科,车辆管理科,统筹规划部,调研科,规划科,人力资源部,人事管理科,劳动工资科,考核培训科,财务部,会计科,审计科,生产作业部,计划调度科,设备动力科,质量管理部,理化实验室,计量检测室,技术研发部,技术管理科,研究开发科,物流控制部,采购科,仓储科,市场营销部,业务管理科,市场拓展科,地域办事处,后勤管理部,总务科,基建科,保健医疗所,信息网络中心";
            DepertmentNames = temp.Split(',');
            #endregion

            #region 单据

            temp = "出差费用报销单,现金日记账 ,原始单据粘贴单,银行日记账,明细帐,总账,三栏账现金借款单,收据,出库单,入库单,领料单,送货单,收款凭证,付款凭证,转账凭证,费用报销单,现金付款单,现金收款单,理费用明细账,多栏账,支票";
            Receipts = temp.Split(',');
            #endregion

            #region 交通工具
            temp = "公共汽车,大客车,私家车,出租汽车,无轨电车,有轨电车,地铁,火车,磁悬浮列车,自行车,摩托车,三轮车,电动车";
            Transportations = temp.Split(',');

            temp = "客轮,货轮,远洋班轮,气垫船,帆船,潜水艇,游艇,救生艇,皮划艇";
            Ships = temp.Split(',');

            temp = "飞机,客机,专机,直升机,宇宙飞船";
            AirPlanes = temp.Split(',');
            #endregion

            #region MoneyUnit 世界货币单位
            MoneyUnit.Add("港元", "HKD");
            MoneyUnit.Add("澳门元", "MOP");
            MoneyUnit.Add("新台币", "TWD");
            MoneyUnit.Add("人民币元", "CNY");
            MoneyUnit.Add("朝鲜圆", "KPW");
            MoneyUnit.Add("韩圆", "KRW");
            MoneyUnit.Add("越南盾", "VND");
            MoneyUnit.Add("日圆", "JPY");
            MoneyUnit.Add("基普", "LAK");
            MoneyUnit.Add("瑞尔", "KHR");
            MoneyUnit.Add("菲律宾比索", "PHP");
            MoneyUnit.Add("马币", "RM");
            MoneyUnit.Add("新加坡元", "SGD");
            MoneyUnit.Add("泰铢", "THP");
            MoneyUnit.Add("缅元", "BUK");
            MoneyUnit.Add("斯里兰卡卢比", "LKR");
            MoneyUnit.Add("马尔代夫卢比", "MVR");
            MoneyUnit.Add("印度尼西亚盾", "IDR");
            MoneyUnit.Add("巴基斯坦卢比", "PRK");
            MoneyUnit.Add("卢比", "INR");
            MoneyUnit.Add("尼泊尔卢比", "NPR");
            MoneyUnit.Add("阿富汗尼", "AFA");
            MoneyUnit.Add("伊朗里亚尔", "IRR");
            MoneyUnit.Add("伊拉克第纳尔", "IQD");
            MoneyUnit.Add("叙利亚镑", "SYP");
            MoneyUnit.Add("黎巴嫩镑", "LBP");
            MoneyUnit.Add("约旦第纳尔", "JOD");
            MoneyUnit.Add("沙特里亚尔", "SAR");
            MoneyUnit.Add("科威特第纳尔", "KWD");
            MoneyUnit.Add("巴林第纳尔", "BHD");
            MoneyUnit.Add("卡塔尔里亚尔", "QAR");
            MoneyUnit.Add("阿曼里亚尔", "OMR");
            MoneyUnit.Add("也门里亚尔", "YER");
            MoneyUnit.Add("也门第纳尔", "YDD");
            MoneyUnit.Add("土耳其镑", "TRL");
            MoneyUnit.Add("塞浦路斯镑", "CYP");
            MoneyUnit.Add("文莱林吉特", "BND");
            MoneyUnit.Add("以色列新谢克尔", "ILS");
            MoneyUnit.Add("蒙古图格里克", "MNT");
            MoneyUnit.Add("阿联酋迪拉姆", "AED");
            MoneyUnit.Add("阿塞拜疆马纳特", "AZN");
            MoneyUnit.Add("不丹努尔特鲁姆", "BTN");
            MoneyUnit.Add("格鲁吉亚拉里", "GEL");
            MoneyUnit.Add("哈萨克斯坦腾格", "KZT");
            MoneyUnit.Add("吉尔吉斯斯坦索姆", "KGS");
            MoneyUnit.Add("孟加拉塔卡", "BDT");
            MoneyUnit.Add("塔吉克斯坦索莫尼", "TJS");
            MoneyUnit.Add("土库曼斯坦马纳特", "TMM");
            MoneyUnit.Add("亚美尼亚德拉姆", "AMD");
            MoneyUnit.Add("乌兹别克斯坦苏姆", "UZS");
            MoneyUnit.Add("欧元", "EUR");
            MoneyUnit.Add("冰岛克朗", "ISK");
            MoneyUnit.Add("丹麦克朗", "DKK");
            MoneyUnit.Add("挪威克朗", "NOK");
            MoneyUnit.Add("瑞典克朗", "SEK");
            MoneyUnit.Add("芬兰马克", "FIM");
            MoneyUnit.Add("卢布", "SUR");
            MoneyUnit.Add("兹罗提", "PLZ");
            MoneyUnit.Add("捷克克朗", "CSK");
            MoneyUnit.Add("福林", "HUF");
            MoneyUnit.Add("马克", "DEM");
            MoneyUnit.Add("奥地利先令", "ATS");
            MoneyUnit.Add("瑞士法郎", "CHF");
            MoneyUnit.Add("荷兰盾", "NLG");
            MoneyUnit.Add("比利时法郎", "BEF");
            MoneyUnit.Add("卢森堡法郎", "LUF");
            MoneyUnit.Add("英镑", "GBP");
            MoneyUnit.Add("爱尔兰镑", "IEP");
            MoneyUnit.Add("法郎", "FRF");
            MoneyUnit.Add("比塞塔", "ESP");
            MoneyUnit.Add("葡萄牙埃斯库多", "PTE");
            MoneyUnit.Add("里拉", "ITL");
            MoneyUnit.Add("马耳他镑", "MTP");
            MoneyUnit.Add("南斯拉夫新第纳尔", "YUD");
            MoneyUnit.Add("列伊", "ROL");
            MoneyUnit.Add("列弗", "BGL");
            MoneyUnit.Add("列克", "ALL");
            MoneyUnit.Add("德拉马克", "GRD");
            MoneyUnit.Add("爱沙尼亚克伦尼", "EEK");
            MoneyUnit.Add("白俄罗斯卢布", "BYR");
            MoneyUnit.Add("塞尔维亚第纳尔", "RSD");
            MoneyUnit.Add("拉脱维亚拉特", "LVL");
            MoneyUnit.Add("马其顿第纳尔", "MKD");
            MoneyUnit.Add("乌克兰格里夫纳", "UAH");
            MoneyUnit.Add("斯洛文尼亚托拉捷夫", "SIT");
            MoneyUnit.Add("斯洛伐克克朗", "Skk");
            MoneyUnit.Add("摩尔多瓦列伊", "MDL");
            MoneyUnit.Add("马其顿第纳尔", "MKD");
            MoneyUnit.Add("加元", "CAD");
            MoneyUnit.Add("美元", "USD");
            MoneyUnit.Add("墨西哥比索", "MXP");
            MoneyUnit.Add("格查尔", "GTQ");
            MoneyUnit.Add("萨尔瓦多科朗", "SVC");
            MoneyUnit.Add("伦皮拉", "HNL");
            MoneyUnit.Add("科多巴", "NIC");
            MoneyUnit.Add("哥斯达黎加科朗", "CRC");
            MoneyUnit.Add("巴拿马巴波亚", "PAB");
            MoneyUnit.Add("古巴比索", "CUP");
            MoneyUnit.Add("巴哈马元", "BSD");
            MoneyUnit.Add("牙买加元", "JMD");
            MoneyUnit.Add("古德", "HTG");
            MoneyUnit.Add("多米尼加比索", "DOP");
            MoneyUnit.Add("特立尼达多巴哥元", "TTD");
            MoneyUnit.Add("巴巴多斯元", "BBD");
            MoneyUnit.Add("哥伦比亚比索", "COP");
            MoneyUnit.Add("博利瓦", "VEB");
            MoneyUnit.Add("圭亚那元", "GYD");
            MoneyUnit.Add("苏里南盾", "SRG");
            MoneyUnit.Add("新索尔", "PES");
            MoneyUnit.Add("苏克雷", "ECS");
            MoneyUnit.Add("新克鲁赛罗", "BRC");
            MoneyUnit.Add("玻利维亚比索", "BOP");
            MoneyUnit.Add("智利比索", "CLP");
            MoneyUnit.Add("阿根廷比索", "ARP");
            MoneyUnit.Add("巴拉圭瓜拉尼", "PYG");
            MoneyUnit.Add("乌拉圭新比索", "UYP");
            MoneyUnit.Add("开曼群岛元", "KYD");
            MoneyUnit.Add("荷属安的列斯盾", "ANG");
            MoneyUnit.Add("伯利兹元", "BZD");
            MoneyUnit.Add("阿鲁巴岛弗罗林", "AWG");
            MoneyUnit.Add("百慕大元", "BMD");
            MoneyUnit.Add("澳大利亚元", "AUD");
            MoneyUnit.Add("新西兰元", "NZD");
            MoneyUnit.Add("斐济元", "FJD");
            MoneyUnit.Add("所罗门元", "SBD");
            MoneyUnit.Add("汤加潘加", "TOP");
            MoneyUnit.Add("巴布亚新几内亚基那", "PGK");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("中非金融合作法郎", "XAF");
            MoneyUnit.Add("埃及镑", "EGP");
            MoneyUnit.Add("利比亚第纳尔", "LYD");
            MoneyUnit.Add("苏丹镑", "SDP");
            MoneyUnit.Add("突尼斯第纳尔", "TND");
            MoneyUnit.Add("阿尔及利亚第纳尔", "DZD");
            MoneyUnit.Add("摩洛哥迪拉姆", "MAD");
            MoneyUnit.Add("乌吉亚", "MRO");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("法拉西", "GMD");
            MoneyUnit.Add("非共体法郎", "XOF");
            MoneyUnit.Add("几内亚西里", "GNS");
            MoneyUnit.Add("利昂", "SLL");
            MoneyUnit.Add("利比里亚元", "LRD");
            MoneyUnit.Add("塞地", "GHC");
            MoneyUnit.Add("奈拉", "NGN");
            MoneyUnit.Add("中非金融合作法郎", "XAF");
            MoneyUnit.Add("中非金融合作法郎", "XAF");
            MoneyUnit.Add("中非金融合作法郎", "XAF");
            MoneyUnit.Add("中非金融合作法郎", "XAF");
            MoneyUnit.Add("中非金融合作法郎", "XAF");
            MoneyUnit.Add("中非金融合作法郎", "XAF");
            MoneyUnit.Add("兰特", "ZAR");
            MoneyUnit.Add("吉布提法郎", "DJF");
            MoneyUnit.Add("索马里先令", "SOS");
            MoneyUnit.Add("肯尼亚先令", "KES");
            MoneyUnit.Add("乌干达先令", "UGS");
            MoneyUnit.Add("坦桑尼亚先令", "TZS");
            MoneyUnit.Add("卢旺达法郎", "RWF");
            MoneyUnit.Add("布隆迪法郎", "BIF");
            MoneyUnit.Add("扎伊尔", "ZRZ");
            MoneyUnit.Add("赞比亚克瓦查", "ZMK");
            MoneyUnit.Add("马达加斯加法郎", "MCF");
            MoneyUnit.Add("塞舌尔卢比", "SCR");
            MoneyUnit.Add("毛里求斯卢比", "MUR");
            MoneyUnit.Add("津巴布韦元", "ZWD");
            MoneyUnit.Add("科摩罗法郎", "KMF");
            MoneyUnit.Add("索马里兰先令", "None");
            MoneyUnit.Add("斯威士兰里兰吉尼", "SZL");
            MoneyUnit.Add("圣赫勒拿群岛磅", "SHP");
            MoneyUnit.Add("圣多美和普林西比多布拉", "STD");
            MoneyUnit.Add("塞拉利昂利昂", "SLL");
            MoneyUnit.Add("纳米比亚元", "NAD");
            MoneyUnit.Add("莫桑比克美提卡", "MZM");
            MoneyUnit.Add("马拉维克瓦查", "MWK");
            MoneyUnit.Add("莱索托马洛蒂", "LSL");
            MoneyUnit.Add("佛得角埃斯库多", "CVE");
            MoneyUnit.Add("厄立特里亚纳克法", "ERN");
            MoneyUnit.Add("非洲金融共同体法郎", "XOF");
            MoneyUnit.Add("博茨瓦纳普拉", "BWP");
            MoneyUnit.Add("安哥拉宽扎", "AOA");
            MoneyUnit.Add("埃塞俄比亚比尔", "ETB");
            #endregion


            #region 各个运营商手机号开头
            temp = "133,142,144,146,148,149,153,180,181,189";
            CTC = temp.Split(',');
            temp = "130,131,132,141,143,145,155,156,185,186";
            CUCC = temp.Split(',');
            temp = "134,135,136,137,138,139,140,147,150,151,152,157,158,159,182,183,187,188";
            CMCC = temp.Split(',');
            #endregion

            #region 地区

            temp = "东北地区,华北地区,华东地区,中南地区,西南地区,西北地区,港澳台地区";
            Region = temp.Split(',');
            {
                temp = "黑龙江省,吉林省,辽宁省";
                var h = temp.Split(',');

                string tempN = "黑,吉,辽";
                var hN = tempN.Split(',');

                for (int i = 0; i < h.Length; i++)
                {
                    Province.Add(h[i], "东北地区");
                    ProvinceNative.Add(hN[i], h[i]);
                }
                temp = "哈尔滨市,齐齐哈尔市,鸡西市,鹤岗市,双鸭山市,大庆市,伊春市,佳木斯市,七台河市,牡丹江市,黑河市,绥化市";
                var city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "黑龙江省");
                }


                temp = "长春,吉林,四平,松原,白城,白山,通化,辽源,延边朝鲜族自治州";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "吉林省");
                }



                temp = "沈阳,大连,鞍山,抚顺,本溪,丹东,锦州,营口,阜新,辽阳,盘锦,铁岭,朝阳,葫芦岛";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "辽宁省");
                }
            }
            {
                temp = "北京市,天津市,山西省,河北省,内蒙古自治区";
                var h = temp.Split(',');
                string tempN = "京,津,晋,冀,蒙";
                var hN = tempN.Split(',');
                for (int i = 0; i < h.Length; i++)
                {
                    Province.Add(h[i], "华北地区");
                    ProvinceNative.Add(hN[i], h[i]);
                }


                temp = "东城区,西城区,朝阳区,丰台区,石景山区,海淀区,顺义区,通州区,大兴区,房山区,门头沟区,昌平区,平谷区,密云区,怀柔区,延庆区";
                var city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "北京市");
                }


                temp = "和平区,河东区,河西区,南开区,河北区,红桥区,东丽区,西青区,津南区,北辰区,武清区,宝坻区,滨海新区,宁河区,静海区,蓟州区";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "天津市");
                }


                temp = "太原市,大同市,阳泉市,长治市,晋城市,朔州市,晋中市,运城市,忻州市,临汾市,吕梁市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "山西省");
                }

                temp = "石家庄市,唐山市,秦皇岛市,邯郸市,邢台市,保定市,张家口市,承德市,张家港市,廊坊市,衡水市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "河北省");
                }



                temp = "呼和浩特市,包头市,乌海市,赤峰市,通辽市,鄂尔多斯市,呼伦贝尔市,巴彦淖尔市,乌兰察布市,兴安盟,锡林郭勒盟,阿拉善盟";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "内蒙古自治区");
                }
            }
            {
                temp = "上海市,江苏省,浙江省,安徽省,福建省,江西省,山东省";
                var h = temp.Split(',');
                string tempN = "沪,苏,浙,皖,闽,赣,鲁";
                var hN = tempN.Split(',');
                for (int i = 0; i < h.Length; i++)
                {
                    Province.Add(h[i], "华东地区");
                    ProvinceNative.Add(hN[i], h[i]);
                }


                temp = "黄浦区,徐汇区,长宁区,静安区,普陀区,虹口区,杨浦区,闵行区,宝山区,嘉定区,浦东新区,金山区,松江区,青浦区,奉贤区,崇明区";
                var city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "上海市");
                }


                temp = "南京市,无锡市,徐州市,常州市,苏州市,南通市,连云港市,淮安市,盐城市,扬州市,镇江市,泰州市,宿迁市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "江苏省");
                }



                temp = "杭州市,宁波市,温州市,嘉兴市,湖州市,绍兴市,金华市,衢州市,舟山市,台州市,丽水市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "浙江省");
                }


                temp = "合肥市,芜湖市,蚌埠市,淮南市,马鞍山市,淮北市,铜陵市,安庆市,黄山市,阜阳市,宿州市,滁州市,六安市,宣城市,池州市,亳州市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "安徽省");
                }

                temp = "福州,厦门,漳州,泉州,莆田,三明,南平,龙岩,宁德";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "福建省");
                }



                temp = "南昌市,九江市,景德镇市,上饶市,鹰潭市,抚州市,宜春市,新余市,萍乡市,吉安市,赣州市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "江西省");
                }


                temp = "济南市,青岛市,淄博市,枣庄市,东营市,烟台市,潍坊市,济宁市,泰安市,威海市,日照市,滨州市,德州市,聊城市,临沂市,菏泽市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "山东省");
                }
            }
            {
                temp = "河南省,湖北省,湖南省";
                var h = temp.Split(',');
                string tempN = "豫,鄂,湘";
                var hN = tempN.Split(',');
                for (int i = 0; i < h.Length; i++)
                {
                    Province.Add(h[i], "中南地区");
                    ProvinceNative.Add(hN[i], h[i]);
                }


                temp = "郑州市,开封市,洛阳市,平顶山市,焦作市,鹤壁市,新乡市,安阳市,濮阳市,许昌市,漯河市,三门峡市,南阳市,商丘市,信阳市,周口市,驻马店市";
                var city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "河南省");
                }

                temp = "武汉市,黄石市,十堰市,宜昌市,襄阳市,鄂州市,荆门市,孝感市,黄冈市,咸宁市,随州市,恩施土家族苗族自治州";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "湖北省");
                }

                temp = "长沙市,株洲市,湘潭市,衡阳市,邵阳市,岳阳市,常德市,张家界市,益阳市,郴州市,永州市,怀化市,娄底市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "湖南省");
                }
            }
            {
                temp = "四川省,贵州省,云南省,重庆市,西藏自治区";
                var h = temp.Split(',');
                string tempN = "川,贵,云,渝,藏";
                var hN = tempN.Split(',');
                for (int i = 0; i < h.Length; i++)
                {
                    Province.Add(h[i], "西南地区");
                    ProvinceNative.Add(hN[i], h[i]);
                }

                temp = "成都市,自贡市,攀枝花市,宜宾市,德阳市,绵阳市,广元市,遂宁市,内江市,乐山市,南充市,眉山市,乐山市,广安市,达州市,雅安市,巴中市和资阳市,包括阿坝藏族羌族自治州,甘孜藏族自治州,凉山彝族自治州";
                var city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "四川省");
                }


                temp = "昆明市,曲靖市,玉溪市,昭通市,保山市,丽江市,普洱市,临沧市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "云南省");
                }


                temp = "万州区,黔江区,涪陵区,渝中区,大渡口区,江北区,沙坪坝区,九龙坡区,南岸区,北碚区,渝北区,巴南区,长寿区,江津区,合川区,永川区,南川区,綦江区,大足区,璧山区,铜梁区,潼南区,荣昌区,开州区,梁平区,武隆区,丰都县";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "重庆市");
                }


                temp = "拉萨市,日喀则市,昌都市,林芝市,山南市和,曲市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "西藏自治区");
                }

                temp = "贵阳市,遵义市,安顺市,六盘水市,铜仁市,毕节市,黔西南布依族苗族自治州,黔东南苗族侗族自治州,黔南布依族苗族自治州";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "贵州省");
                }
            }
            {
                temp = "陕西省,甘肃省,青海省,宁夏回族自治区,新疆维吾尔自治区";
                var h = temp.Split(',');
                string tempN = "陕,甘,青,宁,新";
                var hN = tempN.Split(',');
                for (int i = 0; i < h.Length; i++)
                {
                    Province.Add(h[i], "西北地区");
                    ProvinceNative.Add(hN[i], h[i]);
                }


                temp = "西安市,铜川市,宝鸡市,咸阳市,渭南市,延安市,汉中市,榆林市,安康市,商洛市";
                var city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "陕西省");
                }
                temp = "乌鲁木齐市,克拉玛依市,吐鲁番市,哈密市,阿克苏市,库车市,喀什市,和田市,昌吉市,阜康市,博乐市,阿拉山口市,库尔勒市,阿图什市,伊宁市,奎屯市,霍尔果斯市,塔城市,乌苏市,阿勒泰市,石河子市,阿拉尔市,图木舒克市,五家渠市,北屯市,铁门关市,双河市,可克达拉市,昆玉市,胡杨河市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "新疆维吾尔自治区");
                }
                temp = "银川市,石嘴山市,吴忠市,固原市,中卫市";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "宁夏回族自治区");
                }
                temp = "西宁市,海东市,海北藏族自治州,海南藏族自治州,海西蒙古族藏族自治州,黄南藏族自治州,玉树藏族自治州";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "青海省");
                }

                temp = "兰州市,嘉峪关市,金昌市,白银市,天水市,武威市,张掖市,酒泉市,平凉市,庆阳市,定西市,陇南市,临夏回族自治州,甘南藏族自治州";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "甘肃省");
                }
            }
            {
                temp = "香港特别行政区,澳门特别行政区,台湾省";
                var h = temp.Split(',');
                string tempN = "港,澳,台";
                var hN = tempN.Split(',');
                for (int i = 0; i < h.Length; i++)
                {
                    Province.Add(h[i], "港澳台地区");
                    ProvinceNative.Add(hN[i], h[i]);
                }


                temp = "中环区,湾仔区,铜锣湾区,油尖旺区,深水埗区,黄大仙区";
                var city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "香港特别行政区");
                }
                temp = "台北,新北,桃园,台中,台南,高雄";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "台湾省");
                }
                temp = "门半岛风顺堂区,澳门半岛筷子基区,澳门半岛塔石区,澳门半岛大炮台区,澳门半岛新口岸区,澳门半岛荷兰园区,澳门半岛望德堂区,澳门氹仔区,澳门路环岛,澳门路凼填海区,澳门青洲区,澳门沙湾区和澳门北区";
                city = temp.Split(',');
                for (int i = 0; i < city.Length; i++)
                {
                    City.Add(city[i], "澳门特别行政区");
                }
            }
            #endregion



            temp = "@qq.com,@163.com,@hotmail.com,@yahoo.com,@outlook.com,@gmail.com,@sohu.com";
            EMail = temp.Split(',');

            temp = @"110101,110102,110105,110106,110107,110108,110109,110111,110112,130107,130108,130109,130110,130111,130121,130123,130125,120110,120111,120112,120113,120114,120115,130204,130205,130207,130208,130209,130224,130425,130426,130427,130430,130431,130432,130607,130608,130609,130623,130624,130626,130627,130628,130629,130804,130821,130822,130824,130825,130826,130827,131023,131024,131025,131026,131028,131081,140107,140108,140109,140110,140405,140406,140423,140425,140426,140427,140428,140822,140823,140824,140825,140826,140827,140828,140829,140830,140881,141031,141032,141033,141034,141081,141082,150105,150121,150122,150123,150124,150204,150205,150206,150207,150221,150222,150522,150523,150524,150525,150703,150721,150722,150723,150724,150725,150822,150823,150824,150825,152502,152522,152523,152524,152525,210112,210113,210114,210115,210123,210124,210212,210213,210214,210224,210281,210404,210411,210421,220204,220211,220221,220281,220282,220605,220621,220622,220623,220503,220521,220523,220524,220403,220421,220422,222403,222404,222405,222406,230104,230108,230109,230110,230111,230112,230113,230123,230124,230125,230126,230205,230206,230207,230208,230221,230223,230224,230225,230227,230305,230306,230307,230321,230381,230403,230404,230405,230406,230407,230505,230506,230521,230522,230604,230605,230606,230621,230622,230722,230723,230724,230725,230726,230751,230811,230822,230826,230828,231004,231005,231025,231081,231083,231084,231085";
            RegionCode = temp.Split(',');

            temp = "开发工程师,测试工程师,运维工程师,数据分析师,系统架构师,软件架构师,前端工程师,后端工程师,数据库管理员,网络安全工程师,UI设计师,平面设计师,产品经理,运营经理,项目经理,系统集成工程师";
            ITPosition = temp.Split(',');

            temp = "画龙点睛,半途而废,金玉满堂,人山人海,井底之蛙,一步登天,口是心非,脚踏实地,守株待兔,百折不挠,背水一战,全力以赴,志在千里,坚持不懈,不耻下问,学无止境,一往无前,义无反顾,海纳百川,厚积薄发,集思广益,知己知彼,对症下药,以一当十,以一持万,千锤百炼,胸有成竹,运筹帷幄,神来之笔,耳目一新,视死如归,有口皆碑,大获全胜,八面玲珑,移花接木,大显身手,雨后春笋,有头有脸,雪中送炭,如鱼得水,如虎添翼,知足常乐,举一反三,临危不惧,喜出望外,再接再厉,古为今用,无懈可击,养虎为患,守株待兔,画蛇添足,一箭双雕,一石二鸟,掩耳盗铃,画龙点睛,亡羊补牢,指鹿为马,拔苗助长,一叶障目,惊弓之鸟,掩耳盗铃,画龙点睛,人仰马翻,狐假虎威,指鹿为马";
            Idioms = temp.Split(',');

        }

        #region 统计局指标项

        /// <summary>
        /// 统计局指标项
        /// </summary>
        public string[] TotalIndexs()
        {
            //读取本地的json获取统计局指标并返回
            return new string[1];
        }
        /// <summary>
        /// 统计局细项指标
        /// </summary>
        public string[] TotalSubIndexs()
        {
            //读取本地的json获取统计局指标并返回
            return new string[1];
        }
        /// <summary>
        /// 统计局细项指标
        /// </summary>
        /// <param name="indexName">统计局指标项名称，用于匹配指标项下的所有指标</param>
        /// <returns></returns>
        public string[] TotalSubIndexs(string indexName)
        {
            //读取本地的json获取统计局指标并返回
            return new string[1];
        }
        #endregion



        public string ChineseName()
        {
            return Tool.GetVcodeNum(1, AllNames) + Tool.GetVcodeNum(1, Names);
        }

        public string CTCNumber()
        {
            return Tool.GetVcodeNum(1, CTC) + Tool.GetNum(9);
        }

        public string CUCCNumber()
        {
            return Tool.GetVcodeNum(1, CUCC) + Tool.GetNum(9);
        }

        public string CMCCNumber()
        {
            return Tool.GetVcodeNum(1, CMCC) + Tool.GetNum(9);
        }


        public string PhoneEmailNumber()
        {
            return CMCCNumber() + Tool.GetVcodeNum(1, EMail);
        }

        public string CharEmailNumber()
        {
            return Tool.GetVcodeNum(8) + Tool.GetVcodeNum(1, EMail);
        }

        public string IDCodeNumber()
        {
            return Tool.GetVcodeNum(1, RegionCode) + YearNumber() + MonthNumber() + Tool.GetNum(4);
        }

        public string YearNumber()
        {
            return Tool.GetNum(DateTime.MinValue.Year, DateTime.Now.AddYears(-18).Year);
        }

        public string MonthNumber()
        {
            return Tool.GetNum(1, 12).PadLeft(2, '0');
        }

        public string DayNumber()
        {
            return Tool.GetNum(1, 31).PadLeft(2, '0');
        }

        /// <summary>
        /// 随机True,False
        /// </summary> 
        /// <returns></returns>
        public bool StateData()
        {
            int t = Tool.GetSimpNum(1, 9999);
            return t % 2 == 0;
        }
        public DateTime DataNumber(int day)
        {
            return DateTime.Now.AddDays(Tool.GetSimpNum(1, day) * -1);
        }

        /// <summary>
        /// 矩形内随机一个点
        /// </summary>
        /// <param name="x1">X坐标最小</param>
        /// <param name="x2">X坐标最大</param>
        /// <param name="y1">Y坐标最小</param>
        /// <param name="y2">Y坐标最大</param>
        /// <returns></returns>
        public int[] RectanglePoint(int x1, int x2, int y1, int y2)
        {
            return new int[] { Tool.GetSimpNum(x1, x2), Tool.GetSimpNum(y1, y2) };
        }
        /// <summary>
        /// 获取护照号
        /// 公务护照号码的格式为：SE+7 位数字编码（以S开头的护照是代表公务护照）。
        /// 外交护照号码的格式为：DE+7 位数字编码（以D开头的护照是代表外交护照）。
        /// 公务普通护照号码的格式为：PE+7 位数字编码（以P开头的护照是代表公务普通护照）。
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string IDPostCodeNumber(string format = "PE")
        {
            return format + Tool.GetNum(7);
        }
    }
}
