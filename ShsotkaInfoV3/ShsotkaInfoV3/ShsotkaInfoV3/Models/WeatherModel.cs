using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;


namespace ShsotkaInfoV3.Models
{
    [Serializable]

    [XmlRoot(ElementName = "weatherdata")]
    public class WeatherModel
    {

        [XmlElement(ElementName = "location")]
        public WeatherLocation Location { get; set; }
        [XmlElement(ElementName = "meta")]
        public WeatherMeta Meta { get; set; }
        [XmlElement(ElementName = "forecast")]
        public Forecast Forecast { get; set; }

    }

    public class WeatherLocation
    {
        [XmlElement(ElementName = "name")]
        public string City { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "timezone")]
        public string Timezone { get; set; }
        [XmlElement(ElementName = "location")]
        public LocationContent Location { get; set; }

    }
    public class LocationContent
    {
        [XmlAttribute(AttributeName = "altitude")]
        public string Altitude { get; set; }
        [XmlAttribute(AttributeName = "latitude")]
        public string Latitude { get; set; }
        [XmlAttribute(AttributeName = "longtitude")]
        public string Longtitude { get; set; }
        [XmlAttribute(AttributeName = "geobase")]
        public string Geobase { get; set; }
        [XmlAttribute( AttributeName = "geobaseid")]
        public string GeobaseID { get; set; }
    }
    public class WeatherMeta
    {
        [XmlElement(ElementName = "calctime")]
        public string Calctime { get; set; }
        public Sun Sun { get; set; }
    }

    public class Sun
    {
        [XmlElement(ElementName = "rise")]
        public string Sunrise { get; set; }
        [XmlElement(ElementName = "set")]
        public string Sunset { get; set; }
    }

    public class Forecast
    {
        [XmlElement(ElementName = "time")]
        public List<Weather> Time { get; set; }
    }
    public class Weather
    {
        [XmlAttribute(AttributeName = "from")]
        public string TimeFrom { get; set; }
        [XmlAttribute(AttributeName  = "to")]
        public string TimeTo { get; set; }
        [XmlElement(ElementName = "symbol")]
        public Symbol Symbol { get; set; }
        [XmlElement(ElementName = "precipitation")]
        public Precipitation Precipitation { get; set; }
        [XmlElement(ElementName = "windDirection")]
        public WindDirection WindDirection { get; set; }
        [XmlElement(ElementName = "windSpeed")]
        public WindSpeed WindSpeed { get; set; }
        [XmlElement(ElementName = "temperature")]
        public Temperature Temperature { get; set; }
        [XmlElement(ElementName = "pressure")]
        public Pressure Pressure { get; set; }
        [XmlElement(ElementName = "humidity")]
        public Humidity Humidity { get; set; }
        [XmlElement(ElementName = "clouds")]
        public Clouds Clouds { get; set; }

    }

    public class Clouds
    {
        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }
        [XmlAttribute(AttributeName = "all")]
        public string All { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    public class Humidity
    {
        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    public class Pressure
    {
        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    public class Temperature
    {
        [XmlAttribute(AttributeName  = "unit")]
        public string Unit { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
        [XmlAttribute(AttributeName = "min")]
        public string MinTemp { get; set; }
        [XmlAttribute(AttributeName = "max")]
        public string MaxTemp { get; set; }
    }

    public class WindSpeed
    {
        [XmlAttribute(AttributeName = "mps")]
        public string Mps { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    public class WindDirection
    {
        [XmlAttribute(AttributeName = "deg")]
        public string Deg { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    public class Precipitation
    {
        [XmlAttribute(AttributeName = "value")]
        public string PrecipitationValue { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string PrecipitationType { get; set; }
    }

    public class Symbol
    {
        [XmlAttribute(AttributeName = "name")]
        public string WeatherCond { get; set; }
    }
}
