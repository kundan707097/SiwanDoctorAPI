using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace SiwanDoctorAPI.AppServices.VideoMettingAppServices
{
    public class VideoMettingAppServices: IVideoMettingAppServices
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public VideoMettingAppServices(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessToken()
        {
            var clientId = _configuration["Zoom:ClientId"];
            var clientSecret = _configuration["Zoom:ClientSecret"];
            var accountId = _configuration["Zoom:AccountId"];

            var authUrl = $"https://zoom.us/oauth/token?grant_type=account_credentials&account_id={accountId}";

            var request = new HttpRequestMessage(HttpMethod.Post, authUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"))
            );

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            return tokenResponse.access_token;
        }

        public async Task<string> CreateMeeting(string doctorEmail, string doctorname)
        {
            var accessToken = await GetAccessToken();

            // Set the correct Zoom API endpoint with the doctor's email as the host
            var requestUrl = $"https://api.zoom.us/v2/users/{doctorEmail}/meetings";

            var meetingDetails = new
            {
                topic = $"Doctor Appointment With {doctorname}",
                type = 2, // Scheduled Meeting
                start_time = DateTime.UtcNow.AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = 30,
                timezone = "UTC",
                agenda = "Siwan Doctor Consultation",
                settings = new
                {
                    host_video = true,
                    participant_video = true,
                    mute_upon_entry = true,
                    approval_type = 0
                }
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(meetingDetails), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = requestContent;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var zoomResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            // Modify the join URL to open directly in the browser (avoiding Zoom app download prompt)
            string modifiedJoinUrl = zoomResponse.join_url + "?from=web";

            return modifiedJoinUrl; // Return only the join URL
        }

    }
}
