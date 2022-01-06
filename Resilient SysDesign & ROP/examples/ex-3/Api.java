import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;

public class Api {
    public Result<String, Exception> fetchData() {
        try {
            var data = this.client.send(HttpRequest.newBuilder()
                    .uri(URI.create("http://app.api.com"))
                    .build(), null);

            return new Success<>(data.body().toString());
        } catch (Exception e){
            return new Failure<>(e);
        }
    }

    private HttpClient client;

}
