function infoc(obj)
{
    
//        debugger;
   var this_table=obj.parentNode.parentNode.parentNode;
   var this_tr=obj.parentNode.parentNode;
   var tr_index=obj.parentNode.parentNode.rowIndex;
   var this_td=obj.parentNode; 
   var td_index=obj.parentNode.cellIndex;
   if(tr_index!=1) ////第二,三，四，五行
    {
        
        if(td_index==0)
        {
        //第一列
          var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[0].getElementsByTagName("select")[0].value;
          if(wscode=="NO")
            {
                alert("请选选择上一行的筛选条件！");
                return;
            }
            else
            {
               this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].style.display="none";
               this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[3].getElementsByTagName("select")[0].style.display="block";

               this_td.getElementsByTagName("input")[0].style.display="none";
               this_td.getElementsByTagName("select")[0].style.display="block";
               //this_tr.getElementsByTagName("td")[0].getElementsByTagName("input")[0].style.display="none";
               //this_tr.getElementsByTagName("td")[0].getElementsByTagName("select")[0].style.display="block";
               //this_tr.getElementsByTagName("td")[1].getElementsByTagName("input")[0].style.display="none";
               //this_tr.getElementsByTagName("td")[1].getElementsByTagName("select")[0].style.display="block";
            }
        }
        if(td_index==1)
        {
        //第二列
          var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[0].getElementsByTagName("select")[0].value;
          if(wscode=="NO")
            {
                alert("请选选择上一行的筛选条件！");
                return;
            }
            else
            {
               this_td.getElementsByTagName("input")[0].style.display="none";
               this_td.getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[0].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[0].getElementsByTagName("select")[0].style.display="block";
               //this_tr.getElementsByTagName("td")[1].getElementsByTagName("input")[0].style.display="none";
               //this_tr.getElementsByTagName("td")[1].getElementsByTagName("select")[0].style.display="block";
            }
        }
        if(td_index==3)
        {
        //第三列
          var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[0].getElementsByTagName("select")[0].value;
          if(wscode=="NO")
            {
                alert("请选选择上一行的筛选条件！");
                return;
            }
            else
            {
               this_td.getElementsByTagName("input")[0].style.display="none";
               this_td.getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[0].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[0].getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[1].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[1].getElementsByTagName("select")[0].style.display="block";
            }
        }
    }
    else // 第一行
    {
       this_td.getElementsByTagName("input")[0].style.display="none";
       this_td.getElementsByTagName("select")[0].style.display="block";
       if(td_index==3) //第4列 // 第三列 第一列：名称，第二列：关系，第三列：值，第四列：逻辑
       {
            this_tr.getElementsByTagName("td")[0].getElementsByTagName("input")[0].style.display="none";
            this_tr.getElementsByTagName("td")[0].getElementsByTagName("select")[0].style.display="block";
            this_tr.getElementsByTagName("td")[1].getElementsByTagName("input")[0].style.display="none";
            this_tr.getElementsByTagName("td")[1].getElementsByTagName("select")[0].style.display="block";
       }
       if(td_index==1) //第2列
       {
            this_tr.getElementsByTagName("td")[0].getElementsByTagName("input")[0].style.display="none";
            this_tr.getElementsByTagName("td")[0].getElementsByTagName("select")[0].style.display="block";
       }
    }
}
