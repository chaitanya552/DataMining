from bs4 import BeautifulSoup
from urllib2 import urlopen
import csv

base_url = ("http://www.chicagomag.com/Chicago-Magazine/November-2012/Best-Sandwiches-Chicago/")

soup = BeautifulSoup(urlopen(base_url).read(), "lxml")
sammies = soup.find_all("div", "sammy")
#print sammies
sammy_urls = [div.a["href"] for div in sammies]
print sammy_urls

print "Done writing file"
#this code is giving all the urls of top 50 sandwiches yo!