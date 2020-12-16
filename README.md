# 將 Quartz.Net 排程作業 Hosted 託管於 ASP.NET Core 中，並以 SignalR 實現 Dashboard 頁面 #
當應用網站有一些外部資料需要定時獲得，或是有些內部耗時作業需要批次逐筆消化時，都會需要排程作業來處理，而比較精簡的方式就是將排程作業 Host 在 .NET Core 應用程式上運行，本專案將 Quartz.NET 排程作業 Hosted 託管於 ASP.NET Core 中作為範例；解決了運行問題後所面臨到的就是維運，要如何讓維運人員可以清楚明瞭的掌握目前各個排程作業的執行狀況，這就必須提供一個即時性的 Dashboard 頁面來呈現相關資訊，這部分可透過 SignalR 技術讓 Dashboard 跟後端程式保持一個相互即時主動的溝通渠道，以此避免以往前端定期向後端 Pulling 資料所造成的網路資訊消耗。


<br>
a. 狀態管理頁面

<img src="https://i.imgur.com/KOeWthp.gif" width="600"  /> 


<br>
<br>
b. 在執行中的作業點下 Interrupt 強制中斷作業

<img src="https://i.imgur.com/caz6j4t.gif" width="600"  /> 

<br>
<br>
c. 在已排程的作業點下 Trigger 觸發作業執行

<img src="https://i.imgur.com/c7tv9he.gif" width="600"  /> 

<br>
<br>
d. 關閉排程器及啟動排程器

<img src="https://i.imgur.com/0yoPaMq.gif" width="600"  /> 


