# take input from the console
input1 = input('Enter the String\n') 
inpu = input1
# separate input to left hand side and right hand side of "->"
data = inpu.split("->")
lhs = data[0]
rhs =data[1]
# this gives string rhs length
rhssize = len(rhs)
loop =1
buf = rhs
# this loop works untill the string is split into into two individual characters
while (rhssize - loop > 0) :
  # this b1 gives first character
  b1 = buf[loop - 1]
  # b2 gives the remainder excluding first character
  b2 = buf[loop : rhssize]
  loop= loop + 1
  p1 = b1
  p2 = b2
  a= False
  # checks if first character is lower case
  if 97<=ord(b1[0])<=122:
    # makes the first character a non terminal
    p1 = '<'+b1+'>'	
    a= True
  p2len = len(p2)
  # if no of characters to the right is more thsn 1 then add non terminal to it
  if (p2len >1):		
    p2 = '<'+b2+'>'
  #print("check2")
  full= b1+b2
  # current lhs logic and print statements
  if (rhs == full)  :
    current_lhs = lhs
  else:
    
    current_lhs = '<'+b1+b2+'>'
  print(current_lhs + "->" + p1  + p2) 
  if (a):
    print (p1 +"->"+ b1)