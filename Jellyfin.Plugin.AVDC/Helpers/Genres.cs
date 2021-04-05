using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MediaBrowser.Controller.Providers;

namespace Jellyfin.Plugin.AVDC.Helpers
{
    public static class Genres
    {
        public const string ChineseSubtitle = "中文字幕";

        public static readonly Dictionary<string, string> Substitution = new Dictionary<string, string>
        {
            {"HD", null},
            {"hd", null},
            {"4K", null},
            {"4k", null},
            {"5K", null},
            {"5k", null},
            {"720P", null},
            {"720p", null},
            {"1080P", null},
            {"1080p", null},
            {"60FPS", null},
            {"60fps", null},
            {"10枚组", "10枚組"},
            {"16时间以上作品", "16時間以上作品"},
            {"4小时以上作品", "4小時以上作品"},
            {"AVOPEN2014S级", "AVOPEN2014スーパーヘビー"},
            {"AVOPEN2014重量级", "AVOPEN2014ヘビー級"},
            {"AVOPEN2014中量级", "AVOPEN2014ミドル級"},
            {"AVOPEN2015SM/硬件", "AVOPEN2015SM/ハード部門"},
            {"AVOPEN2015狂热者/恋物癖部门", "AVOPEN2015マニア/フェチ部門"},
            {"AVOPEN2015演员部门", "AVOPEN2015女優部門"},
            {"AVOPEN2015企画部门", "AVOPEN2015企画部門"},
            {"AVOPEN2015熟女部门", "AVOPEN2015熟女部門"},
            {"AVOPEN2015素人部门", "AVOPEN2015素人部門"},
            {"AVOPEN2015少女部", "AVOPEN2015乙女部門"},
            {"AVOPEN2016电视剧纪录部", "AVOPEN2016ドラマ・ドキュメンタリー部門"},
            {"AVOPEN2016ハード部", "AVOPEN2016ハード部門"},
            {"AVOPEN2016娱乐部", "AVOPEN2016バラエティ部門"},
            {"AVOPEN2016疯狂恋物科", "AVOPEN2016マニア・フェチ部門"},
            {"AVOPEN2016演员部", "AVOPEN2016女優部門"},
            {"AVOPEN2016企画部", "AVOPEN2016企画部門"},
            {"AVOPEN2016人妻・熟女部门", "AVOPEN2016人妻・熟女部門"},
            {"AVOPEN2016素人部", "AVOPEN2016素人部門"},
            {"AVOPEN2016少女部", "AVOPEN2016乙女部門"},
            {"AV女优", "AV女優"},
            {"DMM独家", "DMM獨家"},
            {"DMM专属", "DMM專屬"},
            {"DVD多士炉", "DVD多士爐"},
            {"S级女优", "S級女優"},
            {"电影放映", "Vシネマ"},
            {"动作", "アクション"},
            {"绝顶高潮", "アクメ・オーガズム"},
            {"运动员", "アスリート"},
            {"日本动漫", "アニメ"},
            {"日本動漫", "アニメ"},
            {"（视频）男性", "イメージビデオ（男性）"},
            {"片商Emanieru熟女塾", "エマニエル"},
            {"爱的欲望", "エロス"},
            {"御宅族", "オタク"},
            {"手淫", "オナサポ"},
            {"浴室", "お風呂"},
            {"外婆", "お婆ちゃん"},
            {"爷爷", "お爺ちゃん"},
            {"接吻.", "キス・接吻"},
            {"漫画雑志", "コミック雑誌"},
            {"心理惊悚片", "サイコ・スリラー"},
            {"范例影片", "サンプル動画"},
            {"打屁股", "スパンキング"},
            {"智能手机的垂直视频", "スマホ専用縦動画"},
            {"夫妇交换", "スワッピング・夫婦交換"},
            {"性感美女", "セクシー"},
            {"名流", "セレブ"},
            {"啦啦队女孩", "チアガール"},
            {"约会", "デート"},
            {"約會", "デート"},
            {"巨根", "デカチン・巨根"},
            {"不穿内裤", "ノーパン"},
            {"不穿內褲", "ノーパン"},
            {"不穿胸罩", "ノーブラ"},
            {"后宫", "ハーレム"},
            {"高品质VR", "ハイクオリティVR"},
            {"后卫", "バック"},
            {"商务套装", "ビジネススーツ"},
            {"bitch", "ビッチ"},
            {"感恩祭", "ファン感謝・訪問"},
            {"运动短裤", "ブルマ"},
            {"保健香皂", "ヘルス・ソープ"},
            {"男孩恋爱", "ボーイズラブ"},
            {"旅馆", "ホテル"},
            {"旅館", "ホテル"},
            {"妈妈的朋友", "ママ友"},
            {"瑜伽", "ヨガ"},
            {"爱情喜剧", "ラブコメ"},
            {"爱情旅馆", "愛情旅館"},
            {"白领", "白領"},
            {"白眼失神", "白目・失神"},
            {"伴侣", "伴侶"},
            {"办公室", "辦公室"},
            {"办公室美女", "辦公室美女"},
            {"绑缚", "綁縛"},
            {"薄马赛克", "薄馬賽克"},
            {"鼻钩儿", "鼻フック"},
            {"变态", "變態"},
            {"变态游戏", "變態遊戲"},
            {"变性人", "變性者"},
            {"别墅", "別墅"},
            {"医院诊所", "病院・クリニック"},
            {"播音员", "播音員"},
            {"社团经理", "部活・マネージャー"},
            {"残忍画面", "殘忍畫面"},
            {"侧位内射", "側位內射"},
            {"厕所", "廁所"},
            {"插两根", "插兩根"},
            {"插入异物", "插入異物"},
            {"长发", "長發"},
            {"超级女英雄", "超級女英雄"},
            {"车", "車"},
            {"车内", "車內"},
            {"汽车性爱", "車內性愛"},
            {"汽車性愛", "車內性愛"},
            {"车掌小姐", "車掌小姐"},
            {"车震", "車震"},
            {"扯破连裤袜", "扯破連褲襪"},
            {"出轨", "出軌"},
            {"厨房", "廚房"},
            {"处男", "處男"},
            {"处女", "處女"},
            {"处女作", "處女作"},
            {"触手", "觸手"},
            {"搭讪", "搭訕"},
            {"打底裤", "打底褲"},
            {"打飞机", "打飛機"},
            {"打手枪", "打手槍"},
            {"打桩机", "打樁機"},
            {"大学生", "大學生"},
            {"大阴蒂", "大陰蒂"},
            {"单体作品", "單體作品"},
            {"荡妇", "蕩婦"},
            {"第一人称摄影", "第一人稱攝影"},
            {"第一视角", "第一視角"},
            {"店员", "店員"},
            {"电车", "電車"},
            {"电动按摩器", "電動按摩器"},
            {"电动阳具", "電動陽具"},
            {"电话", "電話"},
            {"电梯", "電梯"},
            {"电钻", "電鑽"},
            {"调教", "調教"},
            {"丁字裤", "丁字褲"},
            {"动画", "動画"},
            {"动画人物", "動畫人物"},
            {"动漫", "動漫"},
            {"独立制作", "獨立製作"},
            {"独佔动画", "獨佔動畫"},
            {"堵嘴·喜剧", "堵嘴·喜劇"},
            {"短裤", "短褲"},
            {"恶作剧", "惡作劇"},
            {"儿子", "兒子"},
            {"烦恼", "煩惱"},
            {"房间", "房間"},
            {"访问", "訪問"},
            {"粪便", "糞便"},
            {"风格出众", "風格出眾"},
            {"丰满", "豐滿"},
            {"夫妇", "夫婦"},
            {"服务生", "服務生"},
            {"复刻版", "複刻版"},
            {"蒙面具", "覆面・マスク"},
            {"肛门", "肛門"},
            {"高个子", "高個子"},
            {"哥德萝莉", "哥德蘿莉"},
            {"格斗家", "格鬥家"},
            {"各种职业", "各種職業"},
            {"女性向", "給女性觀眾"},
            {"工作人员", "工作人員"},
            {"公共厕所", "公共廁所"},
            {"公交车", "公交車"},
            {"公园", "公園"},
            {"购物", "購物"},
            {"寡妇", "寡婦"},
            {"灌肠", "灌腸"},
            {"国外进口", "國外進口"},
            {"汗流浃背", "汗だく"},
            {"和服・丧服", "和服・喪服"},
            {"黑暗系统", "黑暗系統"},
            {"黑帮成员", "黑幫成員"},
            {"黑发", "黑髮"},
            {"黑人演员", "黑人演員"},
            {"后入", "後入"},
            {"后入内射", "後入內射"},
            {"护士", "護士"},
            {"花痴", "花癡"},
            {"婚礼", "婚禮"},
            {"及膝袜", "及膝襪"},
            {"极小比基尼", "極小比基尼"},
            {"家庭主妇", "家庭主婦"},
            {"假阳具", "假陽具"},
            {"监禁", "監禁"},
            {"检查", "檢查"},
            {"讲师", "講師"},
            {"娇小", "嬌小"},
            {"娇小的", "嬌小的"},
            {"教师", "教師"},
            {"教学", "教學"},
            {"介绍影片", "介紹影片"},
            {"金发", "金發"},
            {"紧缚", "緊縛"},
            {"紧身衣", "緊身衣"},
            {"经典", "經典"},
            {"经期", "經期"},
            {"精液涂抹", "精液塗抹"},
            {"颈链", "頸鏈"},
            {"痉挛", "痙攣"},
            {"局部特写", "局部特寫"},
            {"巨大阳具", "巨大陽具"},
            {"剧情", "劇情"},
            {"捲发", "捲髮"},
            {"开口器", "開口器"},
            {"看护", "看護"},
            {"可爱", "可愛"},
            {"口内射精", "口內射精"},
            {"啦啦队", "啦啦隊"},
            {"蠟燭", "蝋燭"},
            {"蜡烛", "蝋燭"},
            {"滥交", "濫交"},
            {"烂醉如泥的", "爛醉如泥的"},
            {"牢笼", "牢籠"},
            {"老师", "老師"},
            {"连裤袜", "連褲襪"},
            {"连续内射", "連續內射"},
            {"连衣裙", "連衣裙"},
            {"恋爱", "戀愛"},
            {"恋乳癖", "戀乳癖"},
            {"恋腿癖", "戀腿癖"},
            {"恋物癖", "戀物癖"},
            {"猎艳", "獵豔"},
            {"邻居", "鄰居"},
            {"楼梯", "樓梯"},
            {"乱搞", "亂搞"},
            {"乱交", "亂交"},
            {"乱伦", "亂倫"},
            {"轮奸", "輪姦"},
            {"萝莉", "蘿莉"},
            {"萝莉塔", "蘿莉塔"},
            {"裸体袜子", "裸體襪子"},
            {"裸体围裙", "裸體圍裙"},
            {"妈妈", "媽媽"},
            {"骂倒", "罵倒"},
            {"蛮横娇羞", "蠻橫嬌羞"},
            {"猫耳女", "貓耳女"},
            {"猫眼", "貓眼"},
            {"美容师", "美容師"},
            {"门口", "門口"},
            {"迷你系列", "迷你係列"},
            {"秘书", "秘書"},
            {"密会", "密會"},
            {"面试", "面接"},
            {"面試", "面接"},
            {"苗条", "苗條"},
            {"明星脸", "明星臉"},
            {"模特", "模特兒"},
            {"母亲", "母親"},
            {"男人高潮", "男の潮吹き"},
            {"内裤", "內褲"},
            {"内射", "內射"},
            {"内射潮吹", "內射潮吹"},
            {"内射观察", "內射觀察"},
            {"内衣", "內衣"},
            {"强奸小姨子", "逆レイプ"},
            {"逆强奸", "逆強姦"},
            {"年轻", "年輕"},
            {"年轻人妻", "年輕人妻"},
            {"养女", "娘・養女"},
            {"牛仔裤", "牛仔褲"},
            {"农村", "農村"},
            {"女大学生", "女大學生"},
            {"女检察官", "女檢察官"},
            {"女教师", "女教師"},
            {"女仆", "女僕"},
            {"女体盛", "女體盛"},
            {"女同性恋", "女同性戀"},
            {"女王大人", "女王様"},
            {"女医生", "女醫生"},
            {"女佣", "女傭"},
            {"演员的总编", "女優ベスト・総集編"},
            {"演员按摩棒", "女優按摩棒"},
            {"女战士", "女戰士"},
            {"女装人妖", "女裝人妖"},
            {"偶像艺人", "偶像藝人"},
            {"呕吐", "嘔吐"},
            {"拍摄现场", "拍攝現場"},
            {"泡泡袜", "泡泡襪"},
            {"骗奸", "騙奸"},
            {"贫乳・微乳", "貧乳・微乳"},
            {"妻子出轨", "妻子出軌"},
            {"其他恋物癖", "其他戀物癖"},
            {"骑乘内射", "騎乘內射"},
            {"骑乘位", "騎乘位"},
            {"騎乗位", "騎乘位"},
            {"骑在脸上", "騎在臉上"},
            {"企画", "企畫"},
            {"企划物", "企劃物"},
            {"强奸", "強姦"},
            {"情侣", "情侶"},
            {"情趣内衣", "情趣內衣"},
            {"亲人", "親人"},
            {"求职", "求職"},
            {"人气系列", "人氣系列"},
            {"晒黑", "日焼け"},
            {"软体", "軟体"},
            {"軟體", "軟体"},
            {"润滑剂", "潤滑劑"},
            {"润滑油", "潤滑油"},
            {"赛车女郎", "賽車女郎"},
            {"丧服", "喪服"},
            {"瘙痒", "瘙癢"},
            {"沙发", "沙發"},
            {"晒痕", "曬痕"},
            {"舌头", "舌頭"},
            {"射在头发", "射在頭髮"},
            {"射在外阴", "射在外陰"},
            {"设置项目", "設置項目"},
            {"摄影", "攝影"},
            {"身体意识", "身體意識"},
            {"深肤色", "深膚色"},
            {"绳子", "繩子"},
            {"食粪", "食糞"},
            {"时间停止", "時間停止"},
            {"实拍", "實拍"},
            {"视频聊天", "視頻聊天"},
            {"视讯小姐", "視訊小姐"},
            {"手铐", "手銬"},
            {"首次内射", "首次內射"},
            {"接待小姐", "受付嬢"},
            {"叔母阿姨", "叔母さん"},
            {"束缚", "束縛"},
            {"数位马赛克", "數位馬賽克"},
            {"双性人", "雙性人"},
            {"顺从", "順從"},
            {"私人摄影", "私人攝影"},
            {"丝带", "絲帶"},
            {"送货上门", "送貨上門"},
            {"素颜", "素顏"},
            {"套装", "套裝"},
            {"特典（AV棒球）", "特典あり（AVベースボール）"},
            {"体验忏悔", "體驗懺悔"},
            {"体育服", "體育服"},
            {"舔脚", "舔腳"},
            {"舔阴", "舔陰"},
            {"通奸", "通姦"},
            {"同性恋", "同性戀"},
            {"童颜", "童顔"},
            {"偷窥", "偷窺"},
            {"推荐作品", "推薦作品"},
            {"推销", "推銷"},
            {"袜", "襪"},
            {"外观相似", "外觀相似"},
            {"玩弄肛门", "玩弄肛門"},
            {"晚礼服", "晚禮服"},
            {"网袜", "網襪"},
            {"为智能手机推荐垂直视频", "為智能手機推薦垂直視頻"},
            {"围裙", "圍裙"},
            {"猥亵穿着", "猥褻穿著"},
            {"温泉", "溫泉"},
            {"问卷", "問卷"},
            {"问题", "問題"},
            {"屋顶", "屋頂"},
            {"无码", "無碼"},
            {"无毛", "無毛"},
            {"无套", "無套"},
            {"无套性交", "無套性交"},
            {"西装", "西裝"},
            {"戏剧", "戲劇"},
            {"戲劇x", "戲劇"},
            {"限时降价", "限時降價"},
            {"项圈", "項圈"},
            {"小麦色", "小麥色"},
            {"新娘、年轻妻子", "新娘、年輕妻子"},
            {"性爱", "性愛"},
            {"性伴侣", "性伴侶"},
            {"性别转型·女性化", "性別轉型·女性化"},
            {"性骚扰", "性騷擾"},
            {"露胸", "胸チラ"},
            {"休閒装", "休閒裝"},
            {"羞耻", "羞恥"},
            {"悬挂", "懸掛"},
            {"学生", "學生"},
            {"学生（其他）", "學生（其他）"},
            {"学校", "學校"},
            {"学校泳装", "學校泳裝"},
            {"学校作品", "學校作品"},
            {"鸭嘴", "鴨嘴"},
            {"压力", "壓力"},
            {"亚洲女演员", "亞洲女演員"},
            {"颜面骑乘", "顏面騎乘"},
            {"颜射", "顏射"},
            {"顔射", "顏射"},
            {"顏射x", "顏射"},
            {"眼镜", "眼鏡"},
            {"眼泪", "眼淚"},
            {"阳具腰带", "陽具腰帶"},
            {"阳台", "陽台"},
            {"药物", "藥物"},
            {"业余", "業餘"},
            {"医生", "醫生"},
            {"医院", "醫院"},
            {"已婚妇女", "已婚婦女"},
            {"异物插入", "異物插入"},
            {"阴道放入食物", "陰道放入食物"},
            {"阴道观察", "陰道觀察"},
            {"阴道扩张", "陰道擴張"},
            {"阴屁", "陰屁"},
            {"淫荡", "淫蕩"},
            {"淫乱", "淫亂"},
            {"淫语", "淫語"},
            {"酒会、联谊会", "飲み会・合コン"},
            {"饮尿", "飲尿"},
            {"泳装", "泳裝"},
            {"游戏真人版", "遊戲的真人版"},
            {"诱惑", "誘惑"},
            {"慾求不满", "慾求不滿"},
            {"原作协作", "原作コラボ"},
            {"远程操作", "遠程操作"},
            {"愿望", "願望"},
            {"孕育", "孕ませ"},
            {"孕妇", "孕婦"},
            {"运动", "運動"},
            {"运动系", "運動系"},
            {"再会", "再會"},
            {"展场女孩", "展場女孩"},
            {"站立姿势", "站立姿勢"},
            {"振动", "振動"},
            {"职员", "職員"},
            {"主动", "主動"},
            {"主妇", "主婦"},
            {"主观视角", "主觀視角"},
            {"注视", "注視"},
            {"子宫颈", "子宮頸"},
            {"做家务", "做家務"}
        };

        public static bool HasChineseSubtitle(MovieInfo info)
        {
#if __EMBY__
            var filename = Path.GetFileNameWithoutExtension(info.Name);
#else
            var filename = Path.GetFileNameWithoutExtension(info.Path);
#endif
            return HasChineseSubtitle(filename);
        }

        public static bool HasChineseSubtitle(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return false;

            if (filename.Contains(ChineseSubtitle))
                return true;

            var r = new Regex(@"-cd\d+$",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            filename = r.Replace(filename, string.Empty);

            return filename.Substring(Math.Max(0, filename.Length - 2))
                .Equals("-C", StringComparison.OrdinalIgnoreCase);
        }
    }
}