namespace TkMiddleware.DataObjects
{
    public enum TransactionType
    {
        Death = 0,
        PartialPenMove = 1,
        FullPenMove = 2,
        FullLotTransfer = 3,
        PartialLotTranser = 4,
        PenMovement = 5,
        Shipment = 6,
        Receiving = 7,
        None = 255
    }
}
