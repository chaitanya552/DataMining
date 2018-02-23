package dsa;

import java.util.ArrayList;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.Arrays;
import java.lang.*;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.io.IOException;

public class PhoneticTranscription {
	// HashMap initiliazation to store the phonemes and the respective
	// dictionary word

	public static ArrayList<Integer> start = new ArrayList<Integer>();

	// HashMap initiliazation to store the phonemes and the respective
	// dictionary word
	public static HashMap<String, String> map = new HashMap<String, String>();

	// main function starts here
	public static void main(String[] args) throws FileNotFoundException {

		// Specify your Dictionary file here
		File file = new File("dsa.txt");
		try {

			FileReader fileReader = new FileReader(file);
			BufferedReader bufferedReader = new BufferedReader(fileReader);
			StringBuffer stringBufferName = new StringBuffer();

			String line;
			while ((line = bufferedReader.readLine()) != null) {

				String lineSplit[] = line.split("  ");
				map.put(lineSplit[1], lineSplit[0]);

			}
			BufferedReader bri = null;
			FileReader fri = null;
			String phonemeCurrentLine;
			// specify your phonemes input in a text file
			fri = new FileReader("dsainput.txt");
			bri = new BufferedReader(fri);
			String[] phoneme = new String[1];
			while ((phonemeCurrentLine = bri.readLine()) != null) {

				phoneme = phonemeCurrentLine.split(" ");

			}
			int[][] Z = new int[phoneme.length][phoneme.length];
			// to convert the array structure to a string with phonemes spaced
			StringBuilder build = new StringBuilder();
			for (int p = 0; p < phoneme.length; p++) {
				// System.out.println(phoneme[p]);
				build.append(phoneme[p]);
				build.append(" ");
			}
			String text = build.toString();
			// the string text has our phonemes in spaced format

			for (String temp : phoneme) {
				text.concat(temp);
			}

			for (int a = 0; a < phoneme.length; a++) {

				for (int b = 0; b < phoneme.length - a; b++) {
					int c = b + a;

					String buff[] = Arrays.copyOfRange(phoneme, b, c + 1);
					String temp = new String();
					for (int h = 0; h < buff.length; h++) {
						String temp2 = buff[h];
						temp = temp + " " + temp2;

					}

					if (map.get(temp.trim()) != null) {
						Z[b][c] = 1;
						// here we are checking if entire string from b to c
						// is dictionary word
					} else {
						// we will check if the split words are in
						// dictionary
						int separate = 0;
						for (separate = b; separate < c; separate++) {

							if (Z[b][separate] == 1 && Z[separate + 1][c] == 1) {
								Z[b][c] = 1;
								start.add(separate + 1);

							}

						}

					}

				}

			}
			// for sortting the output
			Set<Integer> has = new HashSet<>();
			has.addAll(start);
			start.clear();
			start.addAll(has);
			start.sort(null);

			for (int i = 0; i < start.size(); i++) {

				// N System.out.println(start.get(i));

			}
			ArrayList<String> Result = new ArrayList<String>();
			ArrayList<String> ResultRev = new ArrayList<String>();

			int n = phoneme.length;
			String tempWord = new String();
			int index = n;
			int tmp1 = start.get(start.size() - 1);

			for (int i = start.size() - 1; i >= 0; i--) {

				tmp1 = start.get(i);
				int k = i;
				int count = 5;

				while (k >= 0 && count >= 0) {

					if (map.get(myToString(
							Arrays.copyOfRange(phoneme, start.get(k), n))
							.trim()) != null) {
						tempWord = myToString(Arrays.copyOfRange(phoneme,
								start.get(k), n));

						index = k;
					}

					k--;
					count--;
					tmp1 = start.get(index);

				}

				if (tmp1 == n)
					break;
				// tmp1=first.get(index);
				Result.add(tempWord);
				n = tmp1;
				i = index;

			}

			Result.add(myToString(Arrays.copyOfRange(phoneme, 0, start.get(0))));

			for (int i = Result.size() - 1; i >= 0; i--) {
				ResultRev.add(Result.get(i));

				System.out.print(map.get(Result.get(i).trim()) + " ");

			}

		} catch (IOException e) {
			e.printStackTrace();

		}

	}

	public static boolean search(String[] Dict, String pattern) {
		boolean x = false;

		for (String temp : Dict) {
			String[] tempSplit = temp.split("\t");
			String temp1 = tempSplit[1];
			if (pattern.equals(temp1)) {

				x = true;
			}
		}

		return x;

	}

	public static String myToString(String[] arr) {

		String output = "";
		for (String str : arr)
			output = output + " " + str;
		return output;

	}

	public static String getSubString(String text, int start, int end) {
		int range = end - start;
		String temp = new String();
		int count = 0;

		for (int i = start; i < text.length(); i++) {
			if (text.charAt(i) == ' ') {

				temp += text.charAt(i);
				count++;
			}
			if (count >= range) {

				break;
			}

		}
		return temp;
	}

	public static void getString(String[] Dict, String pattern) {

		for (String temp : Dict) {
			String[] tempSplit = temp.split("\t");
			String temp1 = tempSplit[1];
			if (pattern.equals(temp1)) {

				System.out.print(tempSplit[0] + " ");
				break;
			}
		}
	}

	public static boolean indexOf(String[] a, String key) {
		int begin = 0;
		int end = a.length - 1;
		while (begin <= end) {

			int mid = begin + (end - begin) / 2;
			String[] tempSplit = a[mid].split("\t");
			if (key.compareTo(tempSplit[1]) < 0)
				end = mid - 1;
			else if (key.compareTo(tempSplit[1]) > 0)
				begin = mid + 1;
			else
				return true;
		}
		return false;

	}

}
