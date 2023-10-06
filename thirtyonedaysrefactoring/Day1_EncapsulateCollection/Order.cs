public class Order
{
    private List<OrderLine> _orderLines = new();
    private int _orderTotals = 0;

    public IEnumerable<OrderLine> OrderLines
    {
        get { return _orderLines; }
    }

    public int OrderTotals
    {
        get { return _orderTotals; }
    }

    public void AddOrderLine(OrderLine orderLine)
    {
        _orderTotals += orderLine.Total;
        _orderLines.Add(orderLine);
    }

    public void RemoveOrderLine(OrderLine orderLine)
    {
        orderLine = _orderLines.Find(o => o == orderLine);
        if (orderLine == null) return;

        _orderTotals -= orderLine.Total;
        _orderLines.Remove(orderLine);
    }
}