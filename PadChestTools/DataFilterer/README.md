# **PadChestTools - Data Filter**

The data filterer is a tool for categorising PadChest data into individual conditions using the spreadsheet. The spreadsheet is dirty and so should be cleaned using a tool such as OpenRefine beforehand.


------------

***Preparation:***

A number of issues are extant in the data. Of primary concern is misspelled labels that will lead to this tool creating more categories than required if left unaddressed.

Furthermore, the data needs to be cleaned so that a CSV file is produced, having been trimmed down to the following 5 columns (preserving column names):
ImageID, ImageDir, PatientID, Projection, and Labels.

The label column is used for categorising, and so should be split by the desired granularity. For example, if COPD was the desired category to be obtained, then ['COPD', 'cardiomegaly'] should become COPD (no quotes required).

------------


***Input:***

The data should be unzipped into their respective directories (52 total), with the CSV accompanying at the root level.
As aforementioned, ensure the CSV is cleaned.


------------


***Output:***

Data will be categorised into the following structure:
* ImageID
	* ImageDir
		* PatientID
			* Projection
				* Labels


------------


***Contact***

The behaviour of this tool can be easily expanded. If there is demand, a version can be released to cover an uncleaned spreadsheet.
Raise an issue if anything is unclear.