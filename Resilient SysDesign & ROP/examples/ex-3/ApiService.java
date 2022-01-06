import java.util.Optional;

public class ApiService {

    private Api api;

    public void ProcessData() {
        var result = api.fetchData();

        Optional.of(result)
                .filter(r -> r instanceof Success)
                .ifPresentOrElse((r) -> {
                    var success = (Success<String, Exception>) r;
                    validateAndSave(success.getData());
                }, () -> {
                    assert result instanceof Failure;
                    var failure = (Failure<String, Exception>) result;
                    var e = failure.getError();
                    logError(e);
                });
    }

    private void validateAndSave(String data) {
    }

    private void logError(Exception e){

    }
}
