namespace AuthReadyAPI.DataLayer.DTOs.AuthResponse
{
    public class Full__AuthResponseDTO
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; internal set; }
    }
}
