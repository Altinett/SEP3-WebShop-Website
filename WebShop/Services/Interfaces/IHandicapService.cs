namespace BlazorWasm.Services; 

public interface IHandicapService {
	bool AccessbilityEnabled { get; set; }
	bool ToggleAccessibility();
}