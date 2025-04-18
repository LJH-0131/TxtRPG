namespace TxtRpg
{
    internal class Program
    {
        public class Creature
        {
            public int level = 1;
            public string level2D => level.ToString("D2");
            public string name = "플레이어 이름";
            public string pClass = "무직";
            public int str = 0;
            public int def = 0;
            public int hp = 0;
            public int eqStr = 0;
            public int eqDef = 0;
            public int eqHp = 0;
            public int gold = 0;

            public void CheckStatus()
            {
                Console.WriteLine("Lv. " + level);
                Console.WriteLine($"{name} ( {pClass} )");
                Console.WriteLine($"공격력 : {str} (+{eqStr})");
                Console.WriteLine($"방어력 : {def} (+{eqDef})");
                Console.WriteLine($"체 력 : {hp} (+{eqHp})");
                Console.WriteLine($"Gold : {gold} G");

            }

            public Inventory playerinventory = new Inventory();
        }

        public class Item
        {
            public string itemName = "아이템 이름";
            public string itemPower => $"공격력 +{plusStr}, 방어력 +{plusDef}, 체력 +{plusHp}";
            public string description = "아이템 설명";
            public bool isEquipItem;
            public bool isEquiped = false;
            public bool isSoldOut = false;
            public int Price = 0;
            public int plusStr = 0;
            public int plusDef = 0;
            public int plusHp = 0;
            public void EquipItemEffect(Creature target, int plusStr, int plusDef, int plusHp)
            {
                target.str += plusStr;
                target.def += plusDef;
                target.hp += plusHp;
                target.eqStr += plusStr;
                target.eqDef += plusDef;
                target.eqHp += plusHp;

                Console.WriteLine($"{itemName}을(를) 장비하여 {target.name}의 능력치가\n공격력 {plusStr}\n방어력 {plusDef}\n체력 {plusHp}\n만큼 변화했습니다.");
            }
            public void UnEquipItemEffect(Creature target, int plusStr, int plusDef, int plusHp)
            {
                target.str -= plusStr;
                target.def -= plusDef;
                target.hp -= plusHp;
                target.eqStr -= plusStr;
                target.eqDef -= plusDef;
                target.eqHp -= plusHp;


                Console.WriteLine($"{itemName}을(를) 장비해제하여 {target.name}의 능력치가\n공격력 -{plusStr}\n방어력 -{plusDef}\n체력 -{plusHp}\n만큼 변화했습니다.");
            }


        }
        public class Store
        {
            public List<Item> storeItems = new List<Item>();

            public void ShowStoreItems()
            {
                Console.WriteLine("[상점 아이템 목록]");
                for(int i = 0; i < storeItems.Count; i++)
                {
                    string soldOutTag = storeItems[i].isSoldOut ? "[Sold Out]\t" : "\t\t";
                    Item storeitem = storeItems[i];
                    Console.WriteLine($"{i+1}. {soldOutTag} {storeItems[i].itemName} | {storeItems[i].itemPower} | {storeItems[i].description} | {storeItems[i].Price} G");
                }
            }

            public void BuyItem(int storeItemNum, Creature buyer)
            {
                if(storeItemNum < 0 || storeItemNum >= storeItems.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    return;
                }
                Item storeSelectedItem = storeItems[storeItemNum];
                if(storeSelectedItem.isSoldOut == false)
                {
                    if(storeSelectedItem.Price <= buyer.gold)
                    {
                        buyer.gold -= storeSelectedItem.Price;
                        buyer.playerinventory.AddItem(storeSelectedItem);
                        Console.WriteLine($"{storeSelectedItem.itemName}의 구매를 완료했습니다.");
                        storeSelectedItem.isSoldOut = true;
                    }
                    else
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
            }
        }

        public class Inventory
        {
            public List<Item> items = new List<Item>();

            public void AddItem(Item item)
            {
                items.Add(item);
                Console.WriteLine($"인벤토리에 {item.itemName}이(가) 추가되었습니다.");
            }
            public void RemoveItem(Item item)
            {
                items.Remove(item);
                Console.WriteLine($"인벤토리에서 {item.itemName}이(가) 제거되었습니다.");
            }
            public void ShowInventory()
            {
                if(items.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비었습니다.\n");
                }
                else
                {
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < items.Count; i++)
                    {
                        string equipedTag = items[i].isEquiped ? "[E]" : "";
                        Console.Write($"\t{i + 1}. {equipedTag} {items[i].itemName} | {items[i].itemPower} | {items[i].description}\n");
                    }
                }
            }
        }

        static void Main()
        {
            Creature Player = new Creature();
            Player.level = 1;
            Player.pClass = "전사";
            Player.str = 10;
            Player.def = 5;
            Player.hp = 100;
            Player.gold = 1500;

            Console.Write("이름을 설정해 주세요.\n>>");
            Player.name = Console.ReadLine();

            Console.WriteLine($"\n스파르타 마을에 오신 것을 환영합니다. {Player.name}.");

            int startdo; 

            do
            {
                Console.WriteLine("\n--------------------------------------------------------------------------------\n");

                Console.WriteLine("이곳에서 무엇을 할지 정할 수 있습니다.\n");
                Console.WriteLine("\t1. 상태 보기\n\t2. 인벤토리 확인하기\n\t3. 상점으로 가기\n\t4. 휴식하기\n\t5. 던전으로 출발하기");
                Console.Write("원하시는 행동을 입력해주세요.\n>>");

                startdo = int.Parse(Console.ReadLine());     //영어 입력했을 때 오류가 나는 것 방지하기 시간부족...

                if(startdo == 1)
                {
                    Console.WriteLine("\n--------------------------------------------------------------------------------\n");

                    Console.WriteLine("\n캐릭터의 정보가 표시됩니다.\n");

                    Player.CheckStatus();

                    Console.WriteLine("\n0. 나가기");
                    Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                    int press0 = int.Parse(Console.ReadLine());
                    while (press0 != 0)
                    {
                        Console.WriteLine("나가시는 숫자 버튼은 0입니다.");
                        Console.Write("원하시는 행동을 입력해주세요.\n>>");
                        press0 = int.Parse(Console.ReadLine());
                    }
                }
                else if (startdo == 2)
                {

                    int outInventory;
                    do
                    {
                        Console.WriteLine("\n--------------------------------------------------------------------------------\n");

                        Console.WriteLine("\n인벤토리");
                        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                        Player.playerinventory.ShowInventory();

                        Console.Write("\n1. 장착 관리\n2. 나가기\n>>");
                        outInventory = int.Parse(Console.ReadLine());
                        if (outInventory == 1)
                        {

                            int outManageInventory;
                            do
                            {
                                Console.WriteLine("\n--------------------------------------------------------------------------------\n");
                                Console.WriteLine("인벤토리 - 장착 관리");
                                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                                Player.playerinventory.ShowInventory();

                                Console.WriteLine("\n0. 나가기\n");
                                Console.Write("원하시는 행동을 입력해주세요.\n>>");
                                outManageInventory = int.Parse(Console.ReadLine());
                                if(outManageInventory == 0)
                                {
                                    break;
                                }
                                else if (outManageInventory < 0 || outManageInventory > Player.playerinventory.items.Count)
                                {
                                    Console.WriteLine("잘못된 입력입니다.");
                                }
                                else
                                {
                                    int itemNum = outManageInventory - 1;
                                    Item selectedItem = Player.playerinventory.items[itemNum];

                                    if (!selectedItem.isEquipItem)
                                    {
                                        Console.WriteLine("장비 아이템이 아닙니다.");//회복 포션같은 소비템이면 사용하게 만들어볼수도?
                                    }

                                    if (!selectedItem.isEquiped)
                                    {
                                        selectedItem.EquipItemEffect(Player, selectedItem.plusStr, selectedItem.plusDef, selectedItem.plusHp);
                                        selectedItem.isEquiped = true;
                                    }
                                    else
                                    {
                                        selectedItem.UnEquipItemEffect(Player, selectedItem.plusStr, selectedItem.plusDef, selectedItem.plusHp);
                                        selectedItem.isEquiped = false;
                                    }
                                }

                            } while (outManageInventory != 0);

                        }
                        else if (outInventory !=2)
                        {
                            Console.WriteLine("1또는 2를 입력해주세요.");
                        }
                    } while (outInventory != 2);

                }
                else if (startdo == 3)
                {
                    Store store = new Store();

                    store.storeItems.Add(new Item
                    {
                        itemName = "수련자 갑옷",
                        plusDef = 5,
                        description = "수련에 도움을 주는 갑옷입니다.",
                        isEquipItem = true,
                        Price = 1000
                    });

                    store.storeItems.Add(new Item
                    {
                        itemName = "무쇠갑옷",
                        plusDef = 9,
                        description = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                        isEquipItem = true,
                        Price = 2000
                    });

                    store.storeItems.Add(new Item
                    {
                        itemName = "스파르타의 갑옷",
                        plusDef = 15,
                        description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                        isEquipItem = true,
                        Price = 3500
                    });

                    store.storeItems.Add(new Item
                    {
                        itemName = "낡은 검",
                        plusStr = 2,
                        description = "쉽게 볼 수 있는 낡은 검 입니다.",
                        isEquipItem = true,
                        Price = 600
                    });

                    store.storeItems.Add(new Item
                    {
                        itemName = "청동 도끼",
                        plusStr = 5,
                        description = "어디선가 사용됐던거 같은 도끼입니다.",
                        isEquipItem = true,
                        Price = 1500
                    });

                    store.storeItems.Add(new Item
                    {
                        itemName = "스파르타의 창",
                        plusStr = 7,
                        description = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                        isEquipItem = true,
                        Price = 2500
                    });

                    int storeItemNum;
                    do
                    {
                        Console.WriteLine("\n--------------------------------------------------------------------------------\n");
                        Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
                        Console.WriteLine($"[보유 골드]\n{Player.gold} G\n");


                        store.ShowStoreItems();

                        Console.WriteLine("\n0. 나가기");

                        Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                        storeItemNum = int.Parse(Console.ReadLine());

                        if(storeItemNum == 0)
                        {
                            break;
                        }
                        store.BuyItem(storeItemNum - 1, Player);

                    } while (storeItemNum != 0);


                }
                else if(startdo == 4)
                {
                    Console.WriteLine("\n--------------------------------------------------------------------------------\n");
                    if (Player.gold >= 500)
                    {
                        Player.gold -= 500;
                        Player.hp = 100;
                        Console.WriteLine("휴식을 완료했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("1부터 5까지의 숫자를 입력해주세요");
                }


            } while (startdo != 5);

            Console.WriteLine("\n--------------------------------------------------------------------------------\n");

            Console.WriteLine("던전으로 떠납니다!");



        }
    }
}
