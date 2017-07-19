/*设计BOM中JS操作*/

var table;
var tr;
var obj;
var objstr;
var list;
var id; 
var intIndex=0;arrList = new Array();
arrlist1=new Array();
arrList2=new Array();


/*新增行操作*/
function chkadd()
{
    var pattem=/^\d+$/;
    var num=document.getElementById("ctl00_PrimaryContent_txtnum").value;
    if(num!="")
    {
        if(pattem.test(num)){
        document.getElementById("ctl00_PrimaryContent_add_id").value="1" ;
        }
        else{alert('输入格式有误!');return false;}
    }
    else
    {
        document.getElementById("ctl00_PrimaryContent_add_id").value="0" ;
        alert('请输入行数!');
        return false;
    }
}
        
/*插入行提示*/
function insert()
{
    table=document.getElementById(getClientId().Id1);
    obj=table.getElementsByTagName("input");
    objstr = '';
    for(var i=0;i<obj.length;i++)
    {
        if(obj[i].type.toLowerCase()=="checkbox" && obj[i].value!="")
        {
           if( obj[i].checked)
           {    
              objstr="checked";
              break;
           }
        }
    }
    if(objstr=="checked")
    {
        document.getElementById(getClientId().Id3).value="1" ;
    }
    else            
    {
        alert("请指定要插入行的位置!");
        return false;
    }
}


/*原始数据输入确定是否删除*/
function check()
{
    table=document.getElementById(getClientId().Id1);
    tr=table.getElementsByTagName("tr");
    var count=0;
    for(var i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
        if(obj.checked)
        {    
           count=i;
           break;
        }
    }
    if(count>0)
    {
       var i=confirm('确定删除吗？');
       if(i==true)
       {
            document.getElementById(getClientId().Id2).value=count ;
       }
       else
       {
            document.getElementById(getClientId().Id2).value="0" ;
            return false;
       }
    }
    else            
    {
        alert("请选择要删除的行！");
        return false;
    }
}

/*原始数据>材料计划 选择一类数据*/
function selectAll(obj)
{
     table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_GridView2");
     if(obj.checked)
     {
         for(i=1;i<table.rows.length;i++)
         {
            objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
            objstr.checked=true;
         }
     }
     else
     {
        for(i=1;i<table.rows.length;i++)
        {
            objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
            objstr.checked=false;
        }
     }
}

/*原始数据 选择一类数据*/
function selectOrgAll(obj)
{
     table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel1_GridView1");
     if(obj.checked)
     {
         for(i=1;i<table.rows.length;i++)
         {
            objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            objstr.checked=true;
         }
     }
     else
     {
        for(i=1;i<table.rows.length;i++)
        {
            objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            objstr.checked=false;
        }
     }
}

/*材料计划*/

function mpId(btnName)
{
    var checknum = 0;
    table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_GridView2");    
    tr=table.getElementsByTagName("tr");    
    for(var i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
       
        if(obj.checked)
        {    
           checknum=i;
           break;
        }
       
    }
        
    for(var i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
        if(obj.checked)
        {
            var marid=tr[i].getElementsByTagName("td")[4];
            if(marid.innerHTML==""||marid.innerHTML=="&nbsp;")
            {
                alert("生成材料计划时物料编码不能为空！");
                for(var j=1;j<tr.length;j++)
                {
                    var tdcheck=tr[j].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
                    if(tdcheck.checked)
                    {
                        tdcheck.checked = false;
                    }
                }          
                return false ;
            }
        }
    }
      
        
    if(checknum>0)
    {
        if(btnName.value=="生成材料计划")
        {
           var j=confirm('确定生成材料计划吗？\r\r提示：\r\r以下所有勾选记录中，【物料编码】不为空的记录将生成材料计划！');
        }
        else if(btnName.value=="材料计划变更")
        {
           var j=confirm('确定变更材料计划吗？\r\r提示：\r\r以下所有勾选记录中，【物料编码】不为空的记录将进行材料计划变更！');
        }
        else if(btnName.valueOf="取消材料计划")
        {
           var j=confirm('确定取消材料计划吗？\r\r提示：\r\r以下所有勾选记录中，【物料编码】不为空的记录将取消材料计划！');
        }
        
        if(j==true)
        {
            document.getElementById("ctl00_PrimaryContent_mpid").value="1" ;
            return true;
        }
        else
        {
            document.getElementById("ctl00_PrimaryContent_mpid").value="0" ;
            return false;
        }
    }
    else
    {
        document.getElementById("ctl00_PrimaryContent_mpid").value="0" ;
        alert('请选择材料!');
        return false;
    }
}

/*原始数据勾选要唯一*/
function chkDelete()
{
    table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel1_GridView1");
    tr=table.getElementsByTagName("tr");
    var check_index;
    var check_counts=0;
    for(var i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
        if(obj.checked)
        {
            check_index=i;
            check_counts++;
        }
    }
////    if(check_counts>1)
////    {
////        alert('只能勾选一项!');
////        for(var i=1;i<tr.length;i++)
////        {
////            obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
////            obj.checked=false
////        }
////        document.getElementById("ctl00_PrimaryContent_deleteId").value="0";
////    }
////    else
////    {
        document.getElementById("ctl00_PrimaryContent_deleteId").value=check_index;
////    }
}

/*材料计划类型（正常或变更）*/
function chkmp(input)
{
    table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_GridView2");
    tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
    var n=parseInt(index)*2-1;
    obj=tr[n].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
    if(obj.checked)
    {
        if(tr[n].getElementsByTagName("td")[0].style.backgroundColor=="red")
        {
            document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_mpsubmit").style.display="none";
            document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_mpChange").style.display="";
            for(var j=1;j<tr.length;j=j+2)
            {
                objstr=tr[j].getElementsByTagName("td")[0].style.backgroundColor;
                if(objstr!="red")
                {
                    tr[j].style.display="none";
                }
            }
        }
        else
        {
            document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_mpChange").style.display="none";
            document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_mpsubmit").style.display="";
            for(var j=1;j<tr.length;j=j+2)
            {
                objstr=tr[j].getElementsByTagName("td")[0].style.backgroundColor;
                if(objstr=="red")
                {
                    tr[j].style.display="none";
                }
            }
        }
    }
}

/*制作明细类型（正常或变更）*/
function dblevent(onchange)
{
    table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel3_GridView4");
    tr=table.getElementsByTagName("tr");
    //alert(onchange.parentNode.parentNode.getElementsByTagName("td")[2].innerHTML);
    var index=onchange.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML;
    var n=parseInt(index)*2-1;
    //obj=tr[index].getElementsByTagName("td")[0].style.backgroundColor;
    var ddl=tr[n].getElementsByTagName("td")[15].getElementsByTagName("select")[0];  
    obj=tr[n].getElementsByTagName("td")[15].getElementsByTagName("select")[0].options[ddl.selectedIndex].value;
    if(obj=="0")
    {
        document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel3_chSubmit").style.display="none";
        for(var i=1;i<tr.length;i=i+2)
        {
            //alert(tr[i].getElementsByTagName("td")[15].innerHTML);
            //alert(tr[i].getElementsByTagName("td")[15].getElementsByTagName("select")[0].id);
            var ddlstr=tr[i].getElementsByTagName("td")[15].getElementsByTagName("select")[0];
            objstr=tr[i].getElementsByTagName("td")[15].getElementsByTagName("select")[0].options[ddlstr.selectedIndex].value;
            if(objstr!="0")
            {
                tr[i].style.display="none";
            }
        }
    }
}

/*原始数据查看确定是否删除*/
function chksure()
{
    var chkid=document.getElementById("ctl00_PrimaryContent_deleteId").value;
    if(chkid!="0")
    {
        var j=confirm('确定删除吗？\r\r提示：\r\r（1）已下推记录不能删除；\r\r（2）删除某一部件将删除该部件下所有记录。\r\r（3）按【序号】删除数据！！！');
        if(j==true)
        {
            document.getElementById("ctl00_PrimaryContent_deleteId").value=chkid;
        }
        else
        {
            return false;
        }
    }
    else            
    {
        alert("请勾选要删除的行！");
        return false;
    }
}
/*原始数据查看结构变更*/
function chkattc()
{
    var chkid=document.getElementById("ctl00_PrimaryContent_deleteId").value;
    if(chkid!="0")
    {
        var j=confirm('确定结构变更吗？\r\r提示：\r\r1、只有已生成明细的正常项才能进行结构变更\r\r2、变更不关联到下级');
        if(j==true)
        {
            document.getElementById("ctl00_PrimaryContent_deleteId").value=chkid;
        }
        else
        {
            return false;
        }
    }
    else            
    {
        alert("请勾选要结构变更的项！\r\r提示:\r\r1、只有已生成明细的正常项才能进行结构变更\r\r2、变更不关联到下级");
        return false;
    }
}

/*原始数据查看多条不提计划*/
function chkwmar()
{
    var chkid=document.getElementById("ctl00_PrimaryContent_deleteId").value;
    if(chkid!="0")
    {
        var j=confirm('确定勾选记录【提交(Y)/不提交(N)】计划？\r\r提示：\r\r1、未提交计划项可进行操作\r\r2、变更不关联到下级');
        if(j==true)
        {
            document.getElementById("ctl00_PrimaryContent_deleteId").value=chkid;
        }
        else
        {
            return false;
        }
    }
    else            
    {
        alert("请勾选要操作的项！\r\r提示:\r\r1、未提交计划项可进行操作\r\r2、变更不关联到下级");
        return false;
    }
}
/*原始数据查看替换项*/
function chkreplace()
{
    var chkid=document.getElementById("ctl00_PrimaryContent_deleteId").value;
    if(chkid!="0")
    {
        var j=confirm('确定勾选记录替换？\r\r提示：未提交计划项可进行替换操作');
        if(j==true)
        {
            document.getElementById("ctl00_PrimaryContent_deleteId").value=chkid;
        }
        else
        {
            return false;
        }
    }
    else            
    {
        alert("请勾选要操作的项！\r\r提示:未提交计划项可进行操作");
        return false;
    }
}
/*原始数据查看关键部件项设置*/
function chkKey()
{
    var chkid=document.getElementById("ctl00_PrimaryContent_deleteId").value;
    if(chkid!="0")
    {
        var j=confirm('确定勾选记录为【关键部件(Y)/非关键部件(N)】？');
        if(j==true)
        {
            document.getElementById("ctl00_PrimaryContent_deleteId").value=chkid;
        }
        else
        {
            return false;
        }
    }
    else            
    {
        alert("请勾选要操作的项！");
        return false;
    }
}



/*验证输入总序格式*/
function verify(input)
{
    var zongxu=document.getElementById(input.id);
    table=document.getElementById(getClientId().Id1);
    tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    if(zongxu.value.trim()!="")
    {
        if(!/^([0-9]+|[0-9]+(\.[1-9]{1}[0-9]*)*)$/g.test(zongxu.value))
        {
            alert( "您输入的总序为"+zongxu.value.trim()+"；输入格式有误,请重新输入！！！\r\r提示：\r\r正确的输入格式为：m.n...(m、n为正整数) ");
            ///////////tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].style.background="";
            //document.getElementById(input.id).value="";
            //////zongxu.focus();
        }
    }
    else
    {
       //alert("总序不能为空！！！");
       document.getElementById(input.id).value="";
       return false;
    }
}
//BOM输入，自动添加序号
function autoZongxu(input)
{
  var obj;
  if(document.getElementById("ckbXuhao")!=null)
  {
     obj=document.getElementById("ckbXuhao");
  }
  else if(document.getElementById("ctl00_PrimaryContent_ckbXuhao")!=null)
  {
     obj=document.getElementById("ctl00_PrimaryContent_ckbXuhao");
  }
  
  if(obj.checked)
  {
    table=document.getElementById(getClientId().Id1);
    tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    if(index==1)
    {
       return;
    }
    var zongxu=table.rows[index-1].cells[4].getElementsByTagName("input")[0].value;
    if(zongxu=="")
    {
       return;
    }
    if(document.getElementById(input.id).value=="")
    {
      if(index<16)
      {
        if(zongxu.indexOf(".")>0)
        {
            var tt=zongxu.split('.');
            var now=parseInt(tt[tt.length-1])+1;
            var temp=zongxu.substring(0,zongxu.lastIndexOf('.'));
            var now_index=temp+"."+now;
            document.getElementById(getClientId().Id1).rows[index].cells[4].getElementsByTagName("input")[0].value=now_index;
        }
        else
        {
            var now_index=parseInt(zongxu)+1;
            document.getElementById(getClientId().Id1).rows[index].cells[4].getElementsByTagName("input")[0].value=now_index;

        }
      }
    }
  }
}


/*验证输入总序格式(批复制数据)*/
function Batchverify(input)
{
    var zongxu=document.getElementById(input.id);
    if(zongxu.value.trim()!="")
    {
        if(!/^([0-9]+|[0-9]+(\.[1-9]{1}[0-9]*)*)$/g.test(zongxu.value))
        {
            alert( "您输入的总序为"+zongxu.value.trim()+"；输入格式有误,请重新输入！！！\r\r提示：\r\r正确的输入格式为：m.n...(m、n为正整数) ");
            /////zongxu.focus();
        }
    }
}


function BatchAutoXuhao(input)
{
    table=document.getElementById("GridView1");
    tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.rowIndex;
    var a=table.rows.length;
    if(index==1)
    {
       return;
    }
    var zongxu=table.rows[index-1].cells[3].getElementsByTagName("input")[0].value;
    if(zongxu=="")
    {
       return;
    }
    if(document.getElementById(input.id).value=="")
    {
      if(index<a)
      {
        if(zongxu.indexOf(".")>0)
        {
            var tt=zongxu.split('.');
            var now=parseInt(tt[tt.length-1])+1;
            var temp=zongxu.substring(0,zongxu.lastIndexOf('.'));
            var now_index=temp+"."+now;
            document.getElementById("GridView1").rows[index].cells[3].getElementsByTagName("input")[0].value=now_index;
        }
        else
        {
            var now_index=parseInt(zongxu)+1;
            document.getElementById("GridView1").rows[index].cells[3].getElementsByTagName("input")[0].value=now_index;
        }
      }
    }
}

/*验证输入总序格式(批复制数据)*/
function CopyEditverify(input)
{
    var zongxu=document.getElementById(input.id);
    var index=input.parentNode.parentNode.rowIndex;
    if(zongxu.value.trim()!="")
    {
        if(!/^([0-9]+|[0-9]+(\.[1-9]{1}[0-9]*)*)$/g.test(zongxu.value))
        {
            alert( "您输入的总序为"+zongxu.value.trim()+"；输入格式有误,请重新输入！！！\r\r提示：\r\r正确的输入格式为：m.n...(m、n为正整数) ");
            /////zongxu.focus();
        }
    }
}

function CopyEditAutoXuhao(input)
{
   if(document.getElementById("ckbXuhao").checked)
   {
        table=document.getElementById(getClientId().Id1);
        tr=table.getElementsByTagName("tr");
        var index=input.parentNode.parentNode.rowIndex;
        var a=table.rows.length;
        if(index==1)
        {
           return;
        }
        var zongxu=table.rows[index-1].cells[4].getElementsByTagName("input")[0].value;
        if(zongxu=="")
        {
           return;
        }
        if(document.getElementById(input.id).value=="")
        {
          if(index<a)
          {
            if(zongxu.indexOf(".")>0)
            {
                var tt=zongxu.split('.');
                var now=parseInt(tt[tt.length-1])+1;
                var temp=zongxu.substring(0,zongxu.lastIndexOf('.'));
                var now_index=temp+"."+now;
                document.getElementById(getClientId().Id1).rows[index].cells[4].getElementsByTagName("input")[0].value=now_index;
            }
            else
            {
                var now_index=parseInt(zongxu)+1;
                document.getElementById(getClientId().Id1).rows[index].cells[4].getElementsByTagName("input")[0].value=now_index;
            }
          }
        }
    }
}


/*js连接sql数据库,判断父级*/
function fsqlCon()
{
    conn=new ActiveXObject("adodb.connection")
    connstr=GetConnection();
    conn.open(connstr);
    if (conn.State==1) 
    {
        comText="select count(BM_ZONGXU) from "+list+" where BM_ENGID='" + id + "' and BM_ZONGXU='"+obj+"' ";
    }
    objrs = conn.Execute(comText);
}


/*js连接sql数据库*/
function sqlCon(){
    conn=new ActiveXObject("adodb.connection")
    connstr=GetConnection();
    conn.open(connstr);
    if (conn.State==1) {
        comText="select BM_ZONGXU from "+list+" where BM_ENGID='" + id + "'";
        objrs = conn.Execute(comText);
        while(!objrs.EOF){
            arrList[intIndex]= objrs.Fields(0).Value;
            intIndex++;
            objrs.moveNext(); // 移到下一个记录点
        }
        objrs.Close(); // 关闭记录集和 
        conn.Close(); // 关闭数据库链接
     }
     else{alert('数据库连接失败!');}
}

/*是否有子任务*/
function childTast()
{
    if(document.getElementById("ctl00_PrimaryContent_GridView1"))
    {
        table=document.getElementById("ctl00_PrimaryContent_GridView1");
//        varMyChildTast();
    }
    else 
    {
//        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_GridView2"))
//        {
//            //接收日期
//            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_GridView2");
//            varChildTast();
//        }
//        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel3_GridView3"))
//        {
//            //技术负责人
//            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel3_GridView3");
//            varChildTast();
//        }
//        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel4_GridView4"))
//        {
//            //船  次
//            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel4_GridView4");
//            varChildTast();
//        }
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel5_GridView5"))
        {
            //工程量
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel5_GridView5");
            QvarChildTast();
        }
//        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel6_GridView6"))
//        {
//            //发运百分比
//            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel6_GridView6");
//            varChildTast();
//        }
//        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel7_GridView7"))
//        {
//            //体  积
//            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel7_GridView7");
//            varChildTast();
//        }
//        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel8_GridView8"))
//        {
//            //制作单位
//            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel8_GridView8");
//            varChildTast();
//        }
    }
}

/*我的任务*/
function myTast()
{
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    if(table!=null)
    {
        tr=table.getElementsByTagName("tr");
        for(var i=1;i<tr.length;i++)
        {
            var obj0=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
            var obj1=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
            obj0.style.display="none";
            obj1.style.display="none";
            var hdobj=tr[i].getElementsByTagName("td")[2].innerHTML;
            if(hdobj.indexOf("-") > 0)
            {
                if(i>1)
                {
                    var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
                    if(hdobj.indexOf(lastobj)==0)
                    {
                        tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="";
                        //tr[i-1].getElementsByTagName("td")[9].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[10].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[11].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[12].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[13].getElementsByTagName("a")[0].style.display="none";
                        for(var j=i;j<tr.length;j++)
                        {
                            var nextobj=tr[j].getElementsByTagName("td")[2].innerHTML;
                            var nextobj0=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
                            var nextobj1=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
                            if(nextobj.indexOf(lastobj) == 0)
                            {
                                nextobj0.style.display="none";
                                nextobj1.style.display="none";
                            }
                            else
                            {
                                i=j-1;
                                break;
                            }
                        }
                    }
                }
                tr[i].getElementsByTagName("td")[14].getElementsByTagName("a")[0].style.display="none";   
            }
            else
            {
                tr[i].getElementsByTagName("td")[9].getElementsByTagName("a")[0].style.display="none";
                tr[i].getElementsByTagName("td")[10].getElementsByTagName("a")[0].style.display="none";
                tr[i].getElementsByTagName("td")[11].getElementsByTagName("a")[0].style.display="none";
                tr[i].getElementsByTagName("td")[12].getElementsByTagName("a")[0].style.display="none";
                tr[i].getElementsByTagName("td")[13].getElementsByTagName("a")[0].style.display="none";   
            }
        }
    }
}

/*判断子父级*/
function varMyChildTast()
{
    if(table)
    {
        //var count=0;
        tr=table.getElementsByTagName("tr");
        for(var i=1;i<tr.length;i++)
        {
            var obj0=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
            var obj1=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
            obj0.style.display="none";
            obj1.style.display="none";
            var hdobj=tr[i].getElementsByTagName("td")[2].innerHTML;
            if(hdobj.indexOf("-") > 0)
            {
                //子级不存在工程拆分
                tr[i].getElementsByTagName("td")[11].getElementsByTagName("a")[0].style.display="none";
                if(i>1)
                {
                    var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
                    if(hdobj.indexOf(lastobj)==0)
                    {
                        //count=0;
                        tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="";
                        //tr[i-1].getElementsByTagName("td")[9].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[10].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[11].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[12].getElementsByTagName("a")[0].style.display="none";
                        //tr[i-1].getElementsByTagName("td")[13].getElementsByTagName("a")[0].style.display="none";
                        for(var j=i;j<tr.length;j++)
                        {
                            var nextobj=tr[j].getElementsByTagName("td")[2].innerHTML;
                            var nextobj0=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
                            var nextobj1=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
                            if(nextobj.indexOf(lastobj) == 0)
                            {
                                nextobj0.style.display="none";
                                nextobj1.style.display="none";
                                //子级不存在工程拆分
                                tr[j].getElementsByTagName("td")[11].getElementsByTagName("a")[0].style.display="none";
                                //count++;
                            }
                            else
                            {
                                //i=i-1+count;
                                i=j-1;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

/*技术员任务判断子父级*/
function TechTast()
{
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    if(table)
    {
        tr=table.getElementsByTagName("tr");
        for(var i=1;i<tr.length;i++)
        {
            var obj0=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
            var obj1=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
            obj0.style.display="none";
            obj1.style.display="none";
            var hdobj=tr[i].getElementsByTagName("td")[2].innerHTML;
            if(hdobj.indexOf("-") > 0)
            {
                if(i>1)
                {
                    var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
                    if(hdobj.indexOf(lastobj)==0)
                    {
                        tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="";
                        for(var j=i;j<tr.length;j++)
                        {
                            var nextobj=tr[j].getElementsByTagName("td")[2].innerHTML;
                            var nextobj0=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
                            var nextobj1=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
                            if(nextobj.indexOf(lastobj) == 0)
                            {
                                nextobj0.style.display="none";
                                nextobj1.style.display="none";
                            }
                            else
                            {
                                i=j-1;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

function varChildTast()
{
    if(table)
    {
        var count=0;
        tr=table.getElementsByTagName("tr");
        for(var i=1+count;i<tr.length;i++)
        {
            var obj0=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
            var obj1=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
            obj0.style.display="none";
            obj1.style.display="none";
            var hdobj=tr[i].getElementsByTagName("td")[2].innerHTML;
            if(hdobj.indexOf("-") > 0)
            {
                if(i>1)
                {
                    var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
                    if(hdobj.indexOf(lastobj)==0)
                    {
                        count=0;
                        tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="";
                        for(var j=i;j<tr.length;j++)
                        {
                            var nextobj=tr[j].getElementsByTagName("td")[2].innerHTML;
                            var nextobj0=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
                            var nextobj1=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
                            if(nextobj.indexOf(lastobj) == 0)
                            {
                                nextobj0.style.display="none";
                                nextobj1.style.display="none";
                                count++;
                            }
                            else
                            {
                                i=i-1+count;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

/*工程量判断*/
function QvarChildTast()
{
    if(table)
    {
         var count=0;
        tr=table.getElementsByTagName("tr");
        var pattem=/^\d+(\.\d+)?$/;
        for(var i=1+count;i<tr.length;i++)
        {
//            var sumwght=0;
            //工程量判断
            
//            var obj0=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
//            var obj1=tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
//            obj0.style.display="none";
//            obj1.style.display="none";
            //var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
            //工程量的理论总重
            var sumwght=tr[i].getElementsByTagName("td")[8].getElementsByTagName("span")[0].innerHTML;
            var fywght=tr[i].getElementsByTagName("td")[9].getElementsByTagName("span")[0].innerHTML;
            if(fywght=="")
            {
                fywght=0;
            }
            if(sumwght=="")
            {
                sumwght=0;
            }
            if(pattem.test(sumwght)&&pattem.test(fywght))
            {
                if(sumwght!=fywght)
                {
//                    tr[i].getElementsByTagName("td")[8].getElementsByTagName("span")[0].style.display="none";
//                    tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].style.display="";
                    tr[i].getElementsByTagName("td")[8].getElementsByTagName("span")[0].style.background="yellow";
                }
                else
                {
//                    tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].style.display="none";
//                    tr[i].getElementsByTagName("td")[8].getElementsByTagName("span")[0].style.display="";
                }
            }
//            var hdobj=tr[i].getElementsByTagName("td")[2].innerHTML;
//            if(hdobj.indexOf("-") > 0)
//            {
//                if(i>1)
//                {
//                    var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
//                    if(hdobj.indexOf(lastobj)==0)
//                    {
//                        count=0;
//                        tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="";
//                        for(var j=i;j<tr.length;j++)
//                        {
//                            var nextobj=tr[j].getElementsByTagName("td")[2].innerHTML;
//                            var nextobj0=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[0];
//                            var nextobj1=tr[j].getElementsByTagName("td")[0].getElementsByTagName("img")[1];
//                            if(nextobj.indexOf(lastobj) == 0)
//                            {
//                                nextobj0.style.display="none";
//                                nextobj1.style.display="none";
//                                count++;
//                                //工程量判断
//                                tr[j].getElementsByTagName("td")[10].getElementsByTagName("input")[0].style.display="none";
//                                totalwght=tr[j].getElementsByTagName("td")[10].getElementsByTagName("span")[0].innerHTML;
//                                if(pattem.test(totalwght))
//                                {
//                                    sumwght+=parseFloat(totalwght);
//                                }
//                                else
//                                {
//                                    //alert('输入格式有误!');
//                                    tr[j].getElementsByTagName("td")[10].getElementsByTagName("span")[0].innerHTML=0;
//                                    sumwght+=parseFloat(tr[j].getElementsByTagName("td")[10].getElementsByTagName("span")[0].innerHTML);
//                                }
//                            }
//                            else
//                            {
//                                //工程量判断
//                                var realtotal=tr[i-1].getElementsByTagName("td")[10].getElementsByTagName("span")[0].innerHTML;
//                                if(pattem.test(realtotal))
//                                {
//                                    if(realtotal!=sumwght)
//                                    {
//                                        tr[i-1].getElementsByTagName("td")[10].getElementsByTagName("span")[0].style.display="none";
//                                        tr[i-1].getElementsByTagName("td")[10].getElementsByTagName("input")[0].style.display="";
//                                        tr[i-1].getElementsByTagName("td")[10].getElementsByTagName("input")[0].style.background="red";
//                                    }
//                                }
//                                i=i-1+count;
//                                break;
//                            }
//                        }
//                    }
//                }
//            }
        }
    }
}


/*任务拆分*/
 function fold(input)
 {
    if(document.getElementById("ctl00_PrimaryContent_GridView1"))
    {
        table=document.getElementById("ctl00_PrimaryContent_GridView1");
        varFold(input);
    }
    else 
    {
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_GridView2"))
        {
            //接收日期
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_GridView2");
            varFold(input);
        }
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel3_GridView3"))
        {
            //技术负责人
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel3_GridView3");
            varFold(input);
        }
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel4_GridView4"))
        {
            //船  次
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel4_GridView4");
            varFold(input);
        }
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel5_GridView5"))
        {
            //工程量
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel5_GridView5");
            varFold(input);
        }
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel6_GridView6"))
        {
            //发运百分比
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel6_GridView6");
            varFold(input);
        }
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel7_GridView7"))
        {
            //体  积
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel7_GridView7");
            varFold(input);
        }
        if(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel8_GridView8"))
        {
            //制作单位
            table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel8_GridView8");
            varFold(input);
        }
    }
   
} 
/*指定折叠展开*/
function varFold(input)
{
    var index=input.parentNode.parentNode.childNodes[1].getElementsByTagName("span")[0].innerHTML;
    obj=input.parentNode.parentNode.childNodes[2].innerHTML;
    //table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");
    var j=parseInt(index)+1;
    if(input.id.indexOf("img0")>0)
    {
        tr[index].getElementsByTagName("td")[0].getElementsByTagName("img")[0].style.display="none";
        tr[index].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="";
        for(var i=j;i<tr.length;i++)
        {
            objstr=tr[i].getElementsByTagName("td")[2].innerHTML;
            if(objstr.indexOf(obj)==0)
            {
                tr[i].style.display="";
            }
            else
            {
                break;
            }
        }
    }
    else
    {
        tr[index].getElementsByTagName("td")[0].getElementsByTagName("img")[0].style.display="";
        tr[index].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="none";
        for(var i=j;i<tr.length;i++)
        {
            objstr=tr[i].getElementsByTagName("td")[2].innerHTML;
            if(objstr.indexOf(obj)==0)
            {
                tr[i].style.display="none";
            }
            else
            {
                break;
            }
        }
    }
}

/*所有折叠展开*/
function varFold1(text)
{
    var text=text;
    tr=table.getElementsByTagName("tr");
    switch(text)
    {
         case 0:
            for(var i=1;i<tr.length;i++)
            {
                tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[0].style.display="none";
                tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="none";
                objstr=tr[i].getElementsByTagName("td")[2].innerHTML;
                if(objstr.indexOf("-") > 0)
                {
                    if(i>1)
                    {
                        var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
                        if(objstr.indexOf(lastobj)==0)
                        {
                            tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[0].style.display="none";
                            tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="";                            
                            for(j=i;j<tr.length;j++)
                            {
                                var objtar=tr[j].getElementsByTagName("td")[2].innerHTML ;
                                if(objtar.indexOf(lastobj) == 0)
                                {
                                    tr[j].style.display="";
                                }
                                else
                                {
                                    i=j-1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            break;
      case 1:
            for(var i=1;i<tr.length;i++)
            {
                tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[0].style.display="none";
                tr[i].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="none";
                objstr=tr[i].getElementsByTagName("td")[2].innerHTML ;
                if(objstr.indexOf("-") > 0)
                {
                    if(i>1)
                    {
                        var lastobj=tr[i-1].getElementsByTagName("td")[2].innerHTML;
                        if(objstr.indexOf(lastobj)==0)
                        {
                            tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[0].style.display="";     
                            tr[i-1].getElementsByTagName("td")[0].getElementsByTagName("img")[1].style.display="none";                              
                            for(j=i;j<tr.length;j++)
                            {
                                var objtar=tr[j].getElementsByTagName("td")[2].innerHTML ;
                                if(objtar.indexOf(lastobj) == 0)
                                {
                                    tr[j].style.display="none"; 
                                }
                                else
                                {
                                    i=j-1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            break;
    }
}

/*制作明细复制总序*/
function copyZongxu(input)
{
    var zongxu=document.getElementById(input.id).value;
    var index=input.parentNode.parentNode.childNodes[1].getElementsByTagName("span")[0].innerHTML;
    var tuhao=input.parentNode.parentNode.childNodes[3].getElementsByTagName("input")[0].value;
    if(zongxu!="")
    {
        document.getElementById("ctl00_PrimaryContent_zongxu").value=zongxu;
        document.getElementById("ctl00_PrimaryContent_index").value=index;
        document.getElementById("ctl00_PrimaryContent_hdbtn").click();
    }
}

/*制作明细复制序序号*/
function copyXuhao(input)
{
    var cbl=document.getElementById("ctl00_PrimaryContent_cbxXuhaoCopy");
    if(cbl.checked==true)
    {
       var xuhao=document.getElementById(input.id).value;
       var index=input.parentNode.parentNode.childNodes[1].getElementsByTagName("span")[0].innerHTML;
       var tuhao=input.parentNode.parentNode.childNodes[3].getElementsByTagName("input")[0].value;
       if(xuhao!="")
       {
        document.getElementById("ctl00_PrimaryContent_zongxu").value=xuhao;
        document.getElementById("ctl00_PrimaryContent_index").value=index;
        document.getElementById("ctl00_PrimaryContent_btnxuhaoQuery").click(); //后台
       }
    }
}


/*装箱单长宽高*/
function setClf(n)
{
    var j=n;
    //var table=document.getElementById("<%=GridView1.ClientID%>");
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr"); 
    var pattem=/^\d+(\.\d+)?$/;
    switch(j)
    {
        case 1:
        {
            for(i=1;i<tr.length;i++)
            {
                 var length=tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
                 var width=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
                 var height=tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
                 if(pattem.test(length))
                 {
                    length=parseFloat(length);
                 }
                 else
                 {
                     length=0;
                     alert('提示:输入数据格式不正确!');
                     return;
                 }
                 var volume=(length*width*height/1000000).toFixed(2);
                 if(volume=="0.00")
                 {
                    volume=0;
                 }
                 tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=volume;
            }
            break;
        }
        case 2:
        {
            for(i=1;i<tr.length;i++)
            {
                var length=tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
                var width=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
                var height=tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
                if(pattem.test(width))
                {
                   width=parseFloat(width);
                }
                else
                {
                    width=0;
                    alert('提示:输入数据格式不正确!');
                    return;
                }
                var volume=(length*width*height/1000000).toFixed(2);
                if(volume=="0.00")
                {
                   volume=0;
                }
                tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=volume;
            }
            break;
        }
        case 3:
        {
            for(i=1;i<tr.length;i++)
            {
                 var length=tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
                 var width=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
                 var height=tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
                 if(pattem.test(height))
                 {
                    height=parseFloat(height);
                 }
                 else
                 {
                     height=0;
                     alert('提示:输入数据格式不正确!');
                     return;
                 }
                var volume=(length*width*height/1000000).toFixed(2);
                if(volume=="0.00")
                {
                    volume=0;
                }
                tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=volume;
            }
            break;
        }
    }
}


/*指定负责人和班组*/
function Chperson(text)
{
    var text=text;
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");
    for(i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        objperson=tr[i].getElementsByTagName("td")[13].getElementsByTagName("select")[0];
        if(obj.checked)
        {
            for(j=1;j<objperson.options.length;j++)
            {
                if(objperson.options[j].value == text) 
                { 
                    objperson.options[j].selected = true; 
                    break; 
                } 

            }
        }
    }
}  
/*指定班组*/
function Chbanzu(text)
{
    var text=text;
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");
    for(i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        objperson=tr[i].getElementsByTagName("td")[14].getElementsByTagName("select")[0];
        if(obj.checked)
        {
            for(j=1;j<objperson.options.length;j++)
            {
                if(objperson.options[j].value == text) 
                { 
                    objperson.options[j].selected = true; 
                    break; 
                } 

            }
        }
    }
}      