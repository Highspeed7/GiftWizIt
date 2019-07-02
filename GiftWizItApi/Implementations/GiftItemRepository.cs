using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class GiftItemRepository : Repository<GiftItem>, IGiftItemRepository
    {
        public GiftItemRepository(ApplicationDbContext context): base(context)
        {

        }

        public async Task<IEnumerable<CombGiftItems>> GetGiftListItems(int gift_list_id, string user_id)
        {
            return await Context.DbGiftItemsObject.FromSql($"SELECT gi.item_id, i.image, gift_list_id, partner_id, Afflt_Link, i.name as itm_name, gl.name as glst_name FROM GList_Items as gi JOIN Links_Items_Partners as lip ON gi.g_list_id IN (SELECT glsh.gift_list_id FROM GiftLists as glsh WHERE glsh.user_id = {user_id} AND glsh.gift_list_id = {gift_list_id}) JOIN Items as i ON gi.item_id = i.item_id JOIN GiftLists as gl ON gl.gift_list_id = gi.g_list_id WHERE lip.item_id = gi.item_id AND gi._deleted = 0").ToListAsync();
        }

        public async Task<CombGiftItems> GetGiftItemDetailsByIdAsync(int gift_list_id, string user_id, int itm_id)
        {
            return await Context.DbGiftItemsObject.FromSql($"SELECT gi.item_id, i.image, gift_list_id, partner_id, Afflt_Link, i.name as itm_name, gl.name as glst_name FROM GList_Items as gi JOIN Links_Items_Partners as lip ON gi.g_list_id IN (SELECT glsh.gift_list_id FROM GiftLists as glsh WHERE glsh.user_id = {user_id} AND glsh.gift_list_id = {gift_list_id}) JOIN Items as i ON gi.item_id = i.item_id JOIN GiftLists as gl ON gl.gift_list_id = gi.g_list_id WHERE lip.item_id = gi.item_id AND i.item_id = {itm_id}").FirstAsync();
        }

        public async Task<GiftItem> GetGiftItemByIdAsync(int item_id)
        {
            return await Context.GiftItems.FirstOrDefaultAsync(gi => gi.Item_Id == item_id);
        }
    }
}
