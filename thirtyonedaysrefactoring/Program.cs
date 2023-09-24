var order = new Order();
var orderLine1 = new OrderLine{ Total=5 };
order.AddOrderLine(orderLine1);

Console.WriteLine("Order count: " + order.OrderLines.Count());
Console.WriteLine("Order totals: " + order.OrderTotals);
var orderLine2 = new OrderLine{ Total=10 };
order.AddOrderLine(orderLine2);
Console.WriteLine("Order count: " + order.OrderLines.Count());
Console.WriteLine("Order totals: " + order.OrderTotals);
order.RemoveOrderLine(orderLine1);
Console.WriteLine("Order count: " + order.OrderLines.Count());
Console.WriteLine("Order totals: " + order.OrderTotals);