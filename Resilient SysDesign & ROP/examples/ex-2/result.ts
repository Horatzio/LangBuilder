export type Success<S> = {
  isSuccess: true;
  obj: S;
};

export type Failure<F> = {
  isSuccess: false;
  error: F;
};

export type Result<S, F> = Success<S> | Failure<F>;
