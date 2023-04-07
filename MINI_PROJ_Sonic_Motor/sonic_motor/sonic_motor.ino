#define Motor_N 7
#define Motor_P 6
#define Motor_EN 5
#define TRIG 4
#define ECHO 3

int Motion = 0;
int Speed = 0;
int cm = 0;

void setup() {
  Serial.begin(115200);
  pinMode(Motor_N, OUTPUT);
  pinMode(Motor_P, OUTPUT);
  pinMode(Motor_EN, OUTPUT);
  pinMode(ECHO, INPUT);
  pinMode(TRIG, OUTPUT);
  digitalWrite(Motor_N, LOW);
  digitalWrite(Motor_P, LOW);
  digitalWrite(Motor_EN, LOW);
}

void loop() {
  cm = Dist_cm();
  if(cm <= 10) {
    Motion = 1;
  }
  // else Motion = 0;
  // Auto(cm);
  
  switch(Motion) {
    case 0:
      Motor_STOP();
      Speed = 0;
      break;
    case 1:
      Motor_CW(255);
      delay(5000);
      Motor_STOP();
      delay(3000);
      Motion = 2;      
      break;
    case 2:  
      Motor_CCW(255);
      delay(5000);
      Motion = 0;
      break;
    default :
      Motor_STOP();
      Speed = 0;
      break;
    }
  Motor_Status();
  delay(100);

}

void Auto(int dist) {
  if (dist <= 10) {
    Motor_CW(255);
    // delay(5000);
    Motor_STOP();
    // delay(3000);
    Motor_CCW(255);
    // delay(5000);
    
    Motor_STOP();
    Speed = 0;
  }
  else {
    Speed = 0;
  }
}

void Motor_CW(int Speed) {
  digitalWrite(Motor_P, HIGH);
  digitalWrite(Motor_N, LOW);
  analogWrite(Motor_EN, Speed);
  // delay(5000);
}

void Motor_CCW(int Speed) {
  digitalWrite(Motor_P, LOW);
  digitalWrite(Motor_N, HIGH);
  analogWrite(Motor_EN, Speed);
  // delay(5000);
}

void Motor_STOP(){
  digitalWrite(Motor_P, LOW);
  digitalWrite(Motor_N, LOW);
  digitalWrite(Motor_EN, LOW);
  // delay(3000);
}

void serialEvent() {
  if(Serial.available()){
    String Rxd = Serial.readStringUntil("\n");
    String cmd = Rxd.substring(1, Rxd.length() -1);
    int comma = cmd.indexOf(",");
    int slash = cmd.lastIndexOf(",");
    Motion = cmd.substring(0, comma).toInt();
    Speed = cmd.substring(comma + 1, slash).toInt();
    cm = cmd.substring(slash + 1).toInt();

    Serial.println(Motion);
    Serial.println(Speed);
    Serial.println(cm);
  }
}

void Motor_Status() {
  Serial.print("@");
  Serial.print(Motion);
  Serial.print(",");
  Serial.print(Speed);
  Serial.print(",");
  Serial.println(cm);
}

unsigned int Dist_cm() {
  unsigned long timer = 0;
  unsigned int dist = 0;

  digitalWrite(TRIG, LOW);
  delayMicroseconds(2);
  digitalWrite(TRIG, HIGH);
  delayMicroseconds(10);
  digitalWrite(TRIG, LOW);
  timer = pulseIn(ECHO, HIGH, 24000);

  if(timer == 0) dist = 400;
  else dist = timer * 0.034 / 2;

  return dist;
}