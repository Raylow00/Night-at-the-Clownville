public interface IPurchasable
{
    void UnlockItem(string itemName);
    bool CheckIfPurchasable(string itemId);
}
