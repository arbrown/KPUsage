# KPUsage
A plugin for KeePass to export simple usage statistics

[Download the latest version](https://github.com/arbrown/KPUsage/releases)
--- 
KeePass keeps track of every time you 'touch' a database entry (auto-type, copy password, open entry, etc...)  Unfortunately, it does not re-write the database automatically for such changes.  So, unless you manually save before you close the database (for instance, after adding another entry), your usage statistics may not get updated.  

Still, I have found these statistics to be a good proxy for accurate usage statistics, and created this plugin to give them some visibility.  This plugin exports a .csv file with the recorded usage statistics for each entry in the database, and it provides some insight into the relative frequency with which each entry is updated.

