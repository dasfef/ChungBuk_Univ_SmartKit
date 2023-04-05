#define Motor_N 7
#define Motor_P 6
#define Motor_EN 5

int Motion = 0;
int Speed = 0;

void setup() {
  Serial.begin(115200);
  pinMode(Motor_N, OUTPUT);
  pinMode(Motor_P, OUTPUT);
  pinMode(Motor_EN, OUTPUT);
  digitalWrite(Motor_N, LOW);
  digitalWrite(Motor_P, LOW);
  digitalWrite(Motor_EN, LOW);
}

void loop() {
  switch(Motion) {
    case 0:
      Motor_STOP();
      Speed = 0;
      break;
    case 1:
      Motor_CW(Speed);
      break;
    case 2:  
      Motor_CCW(Speed);
      break;
    default :
      Motor_STOP();
      Speed = 0;
      break;
    }
  Motor_Status();
  delay(100);

}

void Motor_CW(int Speed) {
  digitalWrite(Motor_P, HIGH);
  digitalWrite(Motor_N, LOW);
  analogWrite(Motor_EN, Speed);
}

void Motor_CCW(int Speed) {
  digitalWrite(Motor_P, LOW);
  digitalWrite(Motor_N, HIGH);
  analogWrite(Motor_EN, Speed);
}

void Motor_STOP(){
  digitalWrite(Motor_P, LOW);
  digitalWrite(Motor_N, LOW);
  digitalWrite(Motor_EN, LOW);
}

void serialEvent() {
  if(Serial.available()){
    String Rxd = Serial.readStringUntil("\n");
    String cmd = Rxd.substring(1, Rxd.length() -1);
    int comma = cmd.indexOf(",");
    Motion = cmd.substring(0, comma).toInt();
    Speed = cmd.substring(comma + 1).toInt();

    Serial.println(Motion);
    Serial.println(Speed);
  }
}

void Motor_Status() {
  Serial.print("@");
  Serial.print(Motion);
  Serial.print(",");
  Serial.println(Speed);
}