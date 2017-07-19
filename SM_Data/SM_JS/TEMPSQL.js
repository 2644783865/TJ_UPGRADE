var conn;
var objrs;
var comText;
var sub_marid;
var tsaid;

/*助记码操作*/
function autoCode(input)
{
    
    var marid=document.getElementById(input.id).value;//助记符
    
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;//行号
    
    if(marid!="")
    {
        var table=document.getElementById("ctl00_PrimaryContent_GridView1");
        
        var tr=table.getElementsByTagName("tr");
        
        sub_marid=marid.substring(0,12);
        
        var child_marid = sub_marid.substring(0,5);//类别编码
        
        document.getElementById(input.id).value=sub_marid;//物料编码
        
        connSql(1);//连接数据库获取物料信息
        //检查是否有数据
        if (!objrs.EOF){
        //MNAME,GUIGE,CAIZHI,GB,TECHUNIT
           
            tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//规格
            tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=objrs.Fields(2).Value;//材质
            tr[index].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=(objrs.Fields(3).Value==null?"":objrs.Fields(3).Value);//国标
            tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=(objrs.Fields(4).Value==null?"":objrs.Fields(4).Value);//单位
            tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value="备库";//计划跟踪号
            if(child_marid == "01.07")   //黑色金属，钢材
            {
               var code=document.getElementById("ctl00_PrimaryContent_LabelCode").innerHTML;

               if(index<10)
               {
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="00"+index+"-"+code; //批号
               }
               if(10<=index<100)
               {
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0"+index+"-"+code; //批号
               }
                if(index>=100)
               {
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=index+"-"+code; //批号
               }
             }
               //仓库
        
            var ws = document.getElementById("ctl00_PrimaryContent_DropDownListWarehouse");
            
            var wscode = "";
            
            for (var i = 0; i < ws.options.length; i++)
            {
                
                if (ws.options[i].selected == true)
                {

                    tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=ws.options[i].text;
                    
                    tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[1].value = ws.options[i].value;
                    
                    tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value="待查";
                    
                    tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[1].value = "0";
                    
                    break;
                }
             }
              //税率
         
            tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value=17; //单价
        }
        else
        {
          document.getElementById(input.id).value="";
          alert("请输入正确的物料编码！");
          return;
        }
        objrs.Close(); // 关闭记录集和 
        conn.Close(); // 关闭数据库链接
        
      
         
      
         
         
         //还需要填写单价与金额
         connSql(2);
         
//如果打开没有记录的 Recordset 对象，BOF 和 EOF 属性将设置为 True，而 Recordset 对象的 RecordCount 属性设置为零。打开至少包含一条记录的 Recordset 对象时，第一条记录为当前记录，而 BOF 和 EOF 属性为 False。

//   使用 RecordCount 属性可确定 Recordset 对象中记录的数目。ADO 无法确定记录数时，或者如果提供者或游标类型不支持 RecordCount，则该属性返回 –1      
    
    // 检查是否有记录 
   
      if (!objrs.EOF){

            while(!objrs.EOF)
            {
                 tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=objrs.Fields("WG_UPRICE").Value; //单价
                 
                 var num=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;//数量
                 
                 tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= parseFloat(objrs.Fields(0).Value)* parseFloat(num);//金额
                
                 objrs.MoveNext;
                 
            }
         }

        objrs.Close(); // 关闭记录集和 
        conn.Close(); // 关闭数据库链接
 
    }
}



/*js连接sql数据库*/
function connSql(n){
    var text=n;
    conn=new ActiveXObject("adodb.connection")
    connstr=GetConnection();
    conn.open(connstr);
    if (conn.State==1) {
         switch(text){
            case 1:
                comText="select MNAME,GUIGE,CAIZHI,GB,PURCUNIT from TBMA_MATERIAL where ID='" + sub_marid + "' and STATE='1' ";
                break;
            case 2:
            
//            select top(1) WG_UPRICE,WG_VERIFYDATE from View_SM_IN where   WG_MARID='01.07.010942' and ( WG_VERIFYDATE  in (select max(WG_VERIFYDATE) from View_SM_IN where WG_MARID='01.07.010942'))
                
                comText="select top(1) WG_UPRICE from View_SM_IN where WG_MARID='" + sub_marid + "' and ( WG_VERIFYDATE  = (select max(WG_VERIFYDATE) from View_SM_IN where WG_MARID='" + sub_marid + "'))";
               
                break;
            default:
                break;
         }
         objrs = conn.Execute(comText);
        }
    }