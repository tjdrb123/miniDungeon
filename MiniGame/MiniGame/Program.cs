internal class Program
{
    private static Character player;
    private static List<Item> myitems;
    private static List<Item> shopitems;
    private static string equipText = "";

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
        myitems = new List<Item>
        {

        };

        shopitems = new List<Item>
        {
            new Item("수련자 갑옷", 0, 5, 1000, "수련에 도움을 주는 갑옷입니다.", "", false,false),
            new Item("무쇠갑옷", 0, 9, 2000, "무쇠로 만들어져 튼튼한 갑옷입니다.", "", false, false),
            new Item("스파르타의 갑옷", 0, 15, 3500, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", "", false, false),
            new Item("낡은 검", 2, 0, 600, "쉽게 볼 수 있는 낡은 검입니다.", "", false, false),
            new Item("청동 도끼", 5, 0, 1500, "어디선가 사용됐던거 같은 도끼입니다.", "", false, false),
            new Item("스파르타의 창", 7, 0, 3000, "스파르타의 전사들이 사용했다는 전설의 창입니다.", "", false , false)
        };
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1,3);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                // 작업해보기
                DisplayInventory();
                break;
            case 3:
                Shop();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        int sumAtK = 0;
        int sumDef = 0;
        for(int i=0; i<myitems.Count; i++)
        {
            if (myitems[i].IsEquip == true)
            {
                sumAtK += myitems[i].ItemAtk;
                sumDef += myitems[i].ItemDef;
            }
        }
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk + sumAtK} +({sumAtK})");
        Console.WriteLine($"방어력 :{player.Def + sumDef} +({sumDef})");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        EquipCheck();
        Console.WriteLine();
        Console.WriteLine("1. 장착관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");


        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
            case 1:
                // 장착 관리 함수
                EquipManagement();
                break;
        }
    }

    static void EquipManagement()
    {
        Console.Clear();
        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("보유 중인 아이템을 장착할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        EquipCheck();

        Console.WriteLine("\n0. 나가기");
        Console.WriteLine("\n아이템 번호를 입력하면 장착/해제 가능합니다.");
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        // 0 - 나가기 버튼, 나머지는 아이템 리스트
        int input = CheckValidInput(0, myitems.Count);
        if (input == 0)
        {
            DisplayInventory();
            // 더 이상 아래 문장을 실행할 이유가 없기때문에 바로 return
            // 코드의 흐름을 더 명확하게 이해 + 가독성 향상 및 함수간의 관계 명확
            return;
        }

        // 선택한 아이템의 장착 여부 업데이트
        myitems[input - 1].IsEquip = !myitems[input - 1].IsEquip;

        EquipManagement(); // 장착 관리 화면을 다시 호출
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }

    static void EquipCheck()
    {
        for (int i = 0; i < myitems.Count; i++)
        {
            // 아이템이 장착되어 있으면 "[E]", 아니면 ""
            if (myitems[i].IsEquip)
            {
                equipText = "[E]";
            }
            else
            {
                equipText = "";
            }

            // 공격력만 가진 아이템, 방어력만 가진 아이템, 공격력 방어력 모두 가진 아이템으로 구분
            if (myitems[i].ItemDef == 0)
            {
                Console.WriteLine($"[{i + 1}] - {equipText}{myitems[i].ItemName} | 공격력 +{myitems[i].ItemAtk} | {myitems[i].ItemInfo}");
            }
            else if (myitems[i].ItemAtk == 0)
            {
                Console.WriteLine($"[{i + 1}] - {equipText}{myitems[i].ItemName} | 방어력 +{myitems[i].ItemDef} | {myitems[i].ItemInfo}");
            }
            else
            {
                Console.WriteLine($"[{i + 1}] - {equipText}{myitems[i].ItemName} | 공격력 +{myitems[i].ItemAtk} | 방어력 +{myitems[i].ItemDef} | {myitems[i].ItemInfo}");
            }
        }
    }

    static void Shop()
    {
        Console.Clear();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G\n");
        Console.WriteLine("아이템 목록");
        for(int i = 0; i<shopitems.Count; i++)
        {
            // 공격력만 가진 아이템, 방어력만 가진 아이템, 공격력 방어력 모두 가진 아이템으로 구분
            if (shopitems[i].ItemDef == 0)
            {
                Console.WriteLine($"- {shopitems[i].ItemName} | 공격력 +{shopitems[i].ItemAtk} | {shopitems[i].ItemInfo}{shopitems[i].BuyMsg} | {shopitems[i].ItemPrice} G");
            }
            else if (shopitems[i].ItemAtk == 0)
            {
                Console.WriteLine($"- {shopitems[i].ItemName} | 방어력 +{shopitems[i].ItemDef} | {shopitems[i].ItemInfo}{shopitems[i].BuyMsg} | {shopitems[i].ItemPrice} G");
            }
            else
            {
                Console.WriteLine($"- {shopitems[i].ItemName} | 공격력 +{shopitems[i].ItemAtk} | 방어력 +{shopitems[i].ItemDef} | {shopitems[i].ItemInfo}{shopitems[i].BuyMsg} | {shopitems[i].ItemPrice} G");
            }
        }

        Console.WriteLine("\n1. 아이템 구매");
        Console.WriteLine("2. 아이템 판매");
        Console.WriteLine("0. 나가기\n");
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                ShopBuyManagement();
                break;
            case 2:
                ShopSellManagement();
                break;

        }
    }

    //아이템 판매 관리
    static void ShopSellManagement()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G\n");
        Console.WriteLine("내가 보유한 아이템 목록");
        for(int i = 0; i<myitems.Count; i++)
        {
            // 공격력만 가진 아이템, 방어력만 가진 아이템, 공격력 방어력 모두 가진 아이템으로 구분
            if (myitems[i].ItemDef == 0)
            {
                Console.WriteLine($"- {i+1} {myitems[i].ItemName} | 공격력 +{myitems[i].ItemAtk} | {myitems[i].ItemInfo} | {myitems[i].ItemPrice} G");
            }
            else if (myitems[i].ItemAtk == 0)
            {
                Console.WriteLine($"- {i + 1} {myitems[i].ItemName} | 방어력 +{myitems[i].ItemDef} | {myitems[i].ItemInfo} | {myitems[i].ItemPrice} G");
            }
            else
            {
                Console.WriteLine($"- {i + 1} {myitems[i].ItemName} | 공격력 +{myitems[i].ItemAtk} | 방어력 +{myitems[i].ItemDef} | {myitems[i].ItemInfo} | {myitems[i].ItemPrice} G");
            }
        }

        Console.WriteLine("\n0. 나가기");
        Console.WriteLine("\n아이템 번호를 선택하면 판매 가능합니다.");
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, myitems.Count);
        if (input == 0)
        {
            Shop();
            return;
        }

        // 팔 수 있으면
        if (myitems[input-1].IsSell == true)
        {

        }
    }

    // 아이템 구매 관리
    static void ShopBuyManagement()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G\n");
        Console.WriteLine("아이템 목록");
        for (int i = 0; i < shopitems.Count; i++)
        {
            // 공격력만 가진 아이템, 방어력만 가진 아이템, 공격력 방어력 모두 가진 아이템으로 구분
            if (shopitems[i].ItemDef == 0)
            {
                Console.WriteLine($"- {i + 1} {shopitems[i].ItemName} | 공격력 +{shopitems[i].ItemAtk} | {shopitems[i].ItemInfo}{shopitems[i].BuyMsg} | {shopitems[i].ItemPrice} G");
            }
            else if (shopitems[i].ItemAtk == 0)
            {
                Console.WriteLine($"- {i + 1} {shopitems[i].ItemName} | 방어력 +{shopitems[i].ItemDef} | {shopitems[i].ItemInfo}{shopitems[i].BuyMsg} | {shopitems[i].ItemPrice} G");
            }
            else
            {
                Console.WriteLine($"- {i + 1} {shopitems[i].ItemName} | 공격력 +{shopitems[i].ItemAtk} | 방어력 +{shopitems[i].ItemDef} | {shopitems[i].ItemInfo}{shopitems[i].BuyMsg} | {shopitems[i].ItemPrice} G");
            }
        }

        Console.WriteLine("\n0. 나가기");
        Console.WriteLine("\n아이템 번호를 선택하면 구매 가능합니다.");
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(0, shopitems.Count);
        if (input == 0)
        {
            for (int i = 0; i < shopitems.Count; i++)
            {
                shopitems[i].BuyMsg = "";
            }
            Shop();
            return;
        }
       
        // 상품이 있으면
        if (shopitems[input-1].IsSell == false)
        {
            // 돈이 있으면
            if(player.Gold >= shopitems[input-1].ItemPrice)
            {
                shopitems[input - 1].IsSell = true;
                myitems.Add(shopitems[input - 1]);
                shopitems[input - 1].BuyMsg = " | 구매를 완료했습니다.";
                player.Gold -= shopitems[input-1].ItemPrice;
            }
            // 돈이 없으면
            else
            {
                shopitems[input - 1].BuyMsg = " | Gold가 부족합니다.";
            }
        }
        // 상품이 없으면
        else
        {
            shopitems[input - 1].BuyMsg = " | 이미 구매한 아이템입니다.";
        }

        ShopBuyManagement();
    }
}


public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; set; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}

public class Item
{
    public string ItemName { get; }
    public int ItemAtk { get; }
    public int ItemDef { get; }
    public int ItemPrice { get; }
    public string ItemInfo { get; }
    public string BuyMsg { get; set; }
    public bool IsEquip { get; set; }
    public bool IsSell { get; set; }

    public Item(string itemname, int itematk, int itemdef, int itemprice, string itemInfo, string buymsg, bool isequip, bool issell)
    {
        ItemName = itemname;
        ItemAtk = itematk;
        ItemDef = itemdef;
        ItemPrice = itemprice;
        ItemInfo = itemInfo;
        BuyMsg = buymsg;
        IsEquip = isequip;
        IsSell = issell;
    }
}