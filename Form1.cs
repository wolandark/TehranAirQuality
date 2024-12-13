using System;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AirQuality
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      dataGridView1.Visible = false;
      FetchAirQualityData();
    }

    private async void FetchAirQualityData()
    {
      try
      {
        // API URL
        string apiUrl = "https://api.waqi.info/feed/tehran/?token=5d5b2e0d0c07cff6625c8ecaa781d38cc831c333";

        using (HttpClient client = new HttpClient())
        {
          // Fetch data
          HttpResponseMessage response = await client.GetAsync(apiUrl);
          response.EnsureSuccessStatusCode();

          string jsonData = await response.Content.ReadAsStringAsync();

          // Parse JSON
          var airQualityData = JsonConvert.DeserializeObject<AirQualityResponse>(jsonData);

          // Display data in DataGridView
          DisplayData(airQualityData);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error: {ex.Message}");
      }
    }

    //private void DisplayData(AirQualityResponse data)
    //{
    //  if (data.Status == "ok")
    //  {
    //    // Add rows to DataGridView
    //    dataGridView1.Columns.Add("Parameter", "Parameter");
    //    dataGridView1.Columns.Add("Value", "Value");

    //    // Add key metrics to table
    //    dataGridView1.Rows.Add("AQI", data.Data.Aqi);
    //    dataGridView1.Rows.Add("Dominant Pollutant", data.Data.Dominentpol);
    //    dataGridView1.Rows.Add("PM2.5", data.Data.Iaqi.Pm25?.V);
    //    dataGridView1.Rows.Add("PM10", data.Data.Iaqi.Pm10?.V);
    //    dataGridView1.Rows.Add("NO2", data.Data.Iaqi.No2?.V);
    //    dataGridView1.Rows.Add("SO2", data.Data.Iaqi.So2?.V);
    //    dataGridView1.Rows.Add("Temperature", data.Data.Iaqi.T?.V);
    //    dataGridView1.Rows.Add("Humidity", data.Data.Iaqi.H?.V);
    //  }
    //  else
    //  {
    //    MessageBox.Show("Failed to fetch air quality data.");
    //  }
    //}

    private void DisplayData(AirQualityResponse data)
    {
      if (data.Status == "ok")
      {
        dataGridView1.RowHeadersVisible = false;
        dataGridView1.ScrollBars = ScrollBars.None;
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // Clear existing columns and rows
        dataGridView1.Columns.Clear();
        dataGridView1.Rows.Clear();

        // Recreate columns
        dataGridView1.Columns.Add("Parameter", "Parameter");
        dataGridView1.Columns.Add("Value", "Value");

        // Add key metrics to table
        dataGridView1.Rows.Add("AQI", data.Data.Aqi);
        dataGridView1.Rows.Add("Dominant Pollutant", data.Data.Dominentpol);
        dataGridView1.Rows.Add("PM2.5", data.Data.Iaqi.Pm25?.V);
        dataGridView1.Rows.Add("PM10", data.Data.Iaqi.Pm10?.V);
        dataGridView1.Rows.Add("NO2", data.Data.Iaqi.No2?.V);
        dataGridView1.Rows.Add("SO2", data.Data.Iaqi.So2?.V);
        dataGridView1.Rows.Add("Temperature", data.Data.Iaqi.T?.V);
        dataGridView1.Rows.Add("Humidity", data.Data.Iaqi.H?.V);

        dataGridView1.Visible = true;

      }
      else
      {
        MessageBox.Show("Failed to fetch air quality data.");
      }
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      FetchAirQualityData();
    }
  }

  // Classes to parse JSON response
  public class AirQualityResponse
  {
    public string Status { get; set; }
    public AirQualityData Data { get; set; }
  }

  public class AirQualityData
  {
    public int Aqi { get; set; }
    public string Dominentpol { get; set; }
    public Iaqi Iaqi { get; set; }
  }

  public class Iaqi
  {
    public Value Pm25 { get; set; }
    public Value Pm10 { get; set; }
    public Value No2 { get; set; }
    public Value So2 { get; set; }
    public Value T { get; set; }
    public Value H { get; set; }
  }

  public class Value
  {
    public double? V { get; set; }
  }
}
