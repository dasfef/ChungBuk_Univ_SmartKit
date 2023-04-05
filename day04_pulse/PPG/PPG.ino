#define PPG A1
unsigned int adPulse;
signed int prevPulse;
signed int diffPulse;

unsigned long sampleMillis = 50;
unsigned long startMillis;
unsigned long nowMillis;

void setup() {
  Serial.begin(115200);
  pinMode(PPG, INPUT);
  startMillis = millis();
  prevPulse = 0;
}

void loop() {
  nowMillis = millis();
  if(nowMillis - startMillis >= sampleMillis){
    adPulse = analogRead(PPG);
    diffPulse = adPulse - prevPulse;
    prevPulse = adPulse;
    startMillis = nowMillis;

    Serial.print("@");
    Serial.print(adPulse);
    Serial.print(",");
    Serial.println(diffPulse);
  }

}
