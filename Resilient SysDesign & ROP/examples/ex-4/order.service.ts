import { Result } from "../ex-2/result";
import { Failure, Success } from "./adapters";

export class OrderService {
  public placeOrder(orderId: string): Result<string, any> {
    try {
      var order = this.findOrder(orderId);
      this.validateOrder(order);
      this.processOrder(order);

      return Success("Order successfully processed!");
    } catch (e) {
      return Failure(e);
    }
  }

  private findOrder(orderId: string): any {}
  private validateOrder(order: any) {}
  private processOrder(order: any) {}
}
