public static class UkaraineService {

	private DateTime _startDate;
	private DateTime _endDate;
	private double _amountOfMoney;
	private List<Person> _whoWantTravel;
	private List<string> _countrysForTravel;

	public UkaraineService(DateTime startDate, DateTime endDate, double amountOfMoney){
		_startDate = startDate;
		_endDate = endDate;
		_amountOfMone = amountOfMoney
		_whoWantTravel = new List<Person>();
		_countrysForTravel = new List<string>();
	}

	public void GetPersonsForTravel() {

		using (var context = new FriendsEntities){
			_whoWantTravel = context.Persons.Select(p => p.IsWantTravel == true).ToList();	
		}
	}

	public void GetCountriesForTravel() {}

	public void GoToCountry(string countryName) {} 
}

public class Person {
	public string Name { get; set; }
	public bool IsMarried { get; set; }
	public bool IsLikeBeer { get; set; }
	public bool IsWantTravel { get; set; }
}