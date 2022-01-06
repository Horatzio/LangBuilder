import { OrderService } from "./order.service";

export class OrderController {
  private orderService: OrderService;

  public placeOrder(orderId: string) {
    const result = this.orderService.placeOrder(orderId);
    if (result.isSuccess) {
      return result.obj;
    } else {
      return "An error has happened!";
    }
  }
}
