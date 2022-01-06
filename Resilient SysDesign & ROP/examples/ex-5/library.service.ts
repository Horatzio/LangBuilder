import {
  BookPriceService,
  ResilientBookPriceService,
} from "./book-price.service.interface";
import { isNumber, isSuccess } from "./type-checks";

interface Author {
  id: string;
  name: string;
  dateOfBirth: Date;
}

interface Book {
  authorId: string;
  bookId: string;
  title: string;
  price: number;
}

export class LibraryService {
  private libraryRepository: LibraryRepository;
  private bookPriceService: BookPriceService;

  public importAuthor(author: Author, books: Book[]) {
    this.libraryRepository.save(author);

    try {
      for (const book of books) {
        // bookPriceService throws error
        book.price = this.bookPriceService.calculatePrice(book);
        this.libraryRepository.save(book);
      }
      // lazy try-catch
    } catch (e) {
      // some books were saved, some not
      throw e;
    }
  }
}

export class ResilientLibaryService {
  private libraryRepository: LibraryRepository;
  private resilientBookPriceService: ResilientBookPriceService;

  public importAuthor(author: Author, books: Book[]) {
    this.libraryRepository.save(author);

    const unsavedBookIds = [];

    for (const book of books) {
      const result = this.resilientBookPriceService.calculatePrice(book);

      if (isSuccess<number>(result, isNumber)) {
        book.price = result.obj;
      } else {
        // I must think of failure as well!
        unsavedBookIds.push(book.bookId);
        console.error(result.error);
      }
      book.price = this.libraryRepository.save(book);
    }
  }
}
