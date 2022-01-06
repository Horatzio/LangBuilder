package Ships.Service;

public abstract class Result<R, F> {
}

public class Success<R, F> extends Result<R, F> {

    private final R result;
    public Success(R result){
        this.result = result;
    }

    public R getData() {
        return this.result;
    }
}

public class Failure<R, F> extends Result<R, F> {

    private final F failure;
    public Failure(F failure){
        this.failure = failure;
    }
    public F getError() {
        return this.failure;
    }
}
