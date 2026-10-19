namespace CodeConventions.Before;

// навмисно поганий стиль для навчального порівняння
public class product {
public string name {get;set;} = "";
public decimal price {get;set;}
public decimal calc(int q){
if(price<0)throw new ArgumentOutOfRangeException(nameof(price));
if(q<0)throw new ArgumentOutOfRangeException(nameof(q));
decimal r=price*q; // множимо ціну на кількість
if(q>=3){r=r*(1-0.10m);}
return r;
}
}
