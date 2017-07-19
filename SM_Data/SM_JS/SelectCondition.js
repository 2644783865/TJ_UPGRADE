function infoc(obj)
    {
    
//        debugger;
       var this_table=obj.parentNode.parentNode.parentNode;
       var this_tr=obj.parentNode.parentNode;
       var tr_index=obj.parentNode.parentNode.rowIndex;
       var this_td=obj.parentNode; 
       var td_index=obj.parentNode.cellIndex;
       if(tr_index!=1)
        {
        //第二行
        if(td_index=0)
        {
        //第一列
          var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[td_index+1].getElementsByTagName("select")[0].value;
          if(wscode=="NO")
            {
                alert("请选选择上一行的筛选条件！");
                return;
            }
            else
            {
               this_td.getElementsByTagName("input")[0].style.display="none";
               this_td.getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[td_index+2].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[td_index+2].getElementsByTagName("select")[0].style.display="block";
            }
        }
        if(td_index=1)
        {
        //第二列
          var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[td_index].getElementsByTagName("select")[0].value;
          if(wscode=="NO")
            {
                alert("请选选择上一行的筛选条件！");
                return;
            }
            else
            {
               this_td.getElementsByTagName("input")[0].style.display="none";
               this_td.getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("select")[0].style.display="block";
            }
        }
        if(td_index=2)
        {
        //第三列
          var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[td_index-1].getElementsByTagName("select")[0].value;
          if(wscode=="NO")
            {
                alert("请选选择上一行的筛选条件！");
                return;
            }
            else
            {
               this_td.getElementsByTagName("input")[0].style.display="none";
               this_td.getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("select")[0].style.display="block";
               this_tr.getElementsByTagName("td")[td_index-2].getElementsByTagName("input")[0].style.display="none";
               this_tr.getElementsByTagName("td")[td_index-2].getElementsByTagName("select")[0].style.display="block";
            }
        }
////          var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[td_index].getElementsByTagName("select")[0].value;
////          if(wscode=="NO")
////            {
////                alert("请选选择上一行的筛选条件！");
////                return;
////            }
////            else
////            {
////               this_td.getElementsByTagName("input")[0].style.display="none";
////               this_td.getElementsByTagName("select")[0].style.display="block";
////               if(td_index!=0)
////               {
////                   this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("input")[0].style.display="none";
////                   this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("select")[0].style.display="block";
////                   if(td_index!=2)
////                   {
////                   this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("input")[0].style.display="none";
////                   this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("select")[0].style.display="block";
////                   }
////                   else
////                   {
////                      var wscode=this_table.getElementsByTagName("tr")[tr_index-1].getElementsByTagName("td")[td_index-1].getElementsByTagName("select")[0].value;
////                      if(wscode=="NO")
////                        {
////                            alert("请选选择上一行的筛选条件！");
////                            return;
////                        }
////                        else
////                        {
////                            this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("input")[0].style.display="none";
////                           this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("select")[0].style.display="block";
////                            this_tr.getElementsByTagName("td")[td_index-2].getElementsByTagName("input")[0].style.display="none";
////                           this_tr.getElementsByTagName("td")[td_index-2].getElementsByTagName("select")[0].style.display="block";
////                       }
////                   }
////               }
////            }
        
        }
        else
        {
               this_td.getElementsByTagName("input")[0].style.display="none";
               this_td.getElementsByTagName("select")[0].style.display="block";
                if(td_index!=2)
               {
               //等于1
                   this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("input")[0].style.display="none";
                   this_tr.getElementsByTagName("td")[td_index+1].getElementsByTagName("select")[0].style.display="block";
               }
                else
               {
                //等于2
                this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("input")[0].style.display="none";
                this_tr.getElementsByTagName("td")[td_index-1].getElementsByTagName("select")[0].style.display="block";
//                this_tr.getElementsByTagName("td")[td_index-2].getElementsByTagName("input")[0].style.display="none";
//                this_tr.getElementsByTagName("td")[td_index-2].getElementsByTagName("select")[0].style.display="block";
               }
        }
    }
