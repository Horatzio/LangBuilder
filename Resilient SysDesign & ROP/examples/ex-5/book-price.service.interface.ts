import { Result } from "../ex-2/result";

export interface BookPriceService {
  calculatePrice(book: any): number;
}

export type BookPriceServiceError = {};

export interface ResilientBookPriceService {
  calculatePrice(book: any): Result<number, BookPriceServiceError>;
}
