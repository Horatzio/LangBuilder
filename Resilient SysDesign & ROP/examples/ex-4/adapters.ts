import { Result } from "../ex-2/result";

export function Success<S, E>(result: S): Result<S, E> {
  return {
    isSuccess: true,
    obj: result,
  };
}

export function Failure<S, E>(error: E): Result<S, E> {
  return {
    isSuccess: false,
    error,
  };
}
