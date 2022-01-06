import { Failure, Success } from "../ex-2/result";

export function isSuccess<S>(
  s: any,
  isS: (obj: any) => obj is S
): s is Success<S> {
  return "isSuccess" in s && s.isSuccess && "obj" in s && isS(s.obj);
}

export function isFailure<F>(
  f: any,
  isF: (obj: any) => obj is F
): f is Failure<F> {
  return "isSuccess" in f && !f.isSuccess && "error" in f && isF(f.error);
}

export function isNumber(o: any): o is number {
  return typeof o === "number";
}
