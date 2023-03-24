#define pin A0
int ADC_Data[20] = {3, 45, 75, 90, 109, 123, 135, 144, 157, 169, 178, 241, 281, 322, 346, 365, 393, 402, 409, 422};
int LUX_Data[20] = {0,  1,  2,  3,   4,   5,   6,   7,   8,   9,  10,  20,  30,  40,  50,  60,  70,  80,  90, 100};

void setup() {
  pinMode(0, INPUT);
  Serial.begin(115200);
}

void loop() {
  int CdS = analogRead(pin);
  int Lux = AdcLux(CdS);
  Serial.print(CdS);
  Serial.print(" - ");
  Serial.println(Lux);
  delay(250);
}

int AdcLux(int AdValue) {
  int LuxValue = 0;
  char k;

  for (k = 0; k < 20; k++) {
    if(AdValue < ADC_Data[k]) break;
  }

  if(k < 2) {
    LuxValue = 0;
  }
  else if (k < 11) {
    LuxValue = LUX_Data[k-1];
  }
  else if (k > 19) {
    LuxValue = 100;
  }
  else {
    LuxValue = LUX_Data[k-1] + (LUX_Data[k] - LUX_Data[k-1]) * (AdValue - ADC_Data[k-1]) / (ADC_Data[k] - ADC_Data[k-1]);
  }
  return LuxValue;
}