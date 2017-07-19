
 //**********弹出技术部人员子窗口***********************
    function SelTechPersons1()
    {
       var i=window.showModalDialog('PC_TBPC_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       document.getElementById('<%=Tb_shenheren1.ClientID%>').innerText=i;
      
    }
    function SelTechPersons2()
    {
        var i=window.showModalDialog('PC_TBPC_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       document.getElementById('<%=Tb_shenheren2.ClientID%>').innerText=i;
    }
    function SelTechPersons3()
    {
        var i=window.showModalDialog('PC_TBPC_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       document.getElementById('<%=Tb_shenheren3.ClientID%>').innerText=i;
    }
    function String.prototype.Trim()   
   {
         return this.replace(/\s+/g,"");
    } 
    
    function changepj(obj)
    {
        var pjnm;
        var pjid;
        pjnm=obj.value;
        if(pjnm.indexOf("|")>0)
        {
            var str=pjnm.split("|");
            pjnm=str[0];
            pjid=str[1];
            obj.value=pjnm;
            document.getElementById("ctl00_PrimaryContent_txb_pjid").value=pjid;
        }
    }
    
    function changeeng(obj)
    {
        var engnm;
        var engid;
        engnm=obj.value;
        if(engnm.indexOf("|")>0)
        {
            var str=engnm.split("|");
            engnm=str[0];
            engid=str[1];
            obj.value=engnm;
            document.getElementById("ctl00_PrimaryContent_txb_engid").value=engid;
        }
    }
    
    function textchange(obj)
    {
         table=document.getElementById("tab");         tr=table.getElementsByTagName("tr");
         var marid;
         var marnm;
         var margg;
         var marcz;
         var margb;
         var marunit;
         var marfzunit;
         marid=obj.value;
         var i=obj.parentNode.parentNode.rowIndex;
         
         if(marid.indexOf("|")>0)
         {
             var str=marid.split("|");
             marid=str[0];
             marnm=str[1];
             margg=str[2];
             marcz=str[3];
             margb=str[4];
             marunit=str[5];
             marfzunit=str[6];
             tr[i].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value=marid;
             tr[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=marnm;
             tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=margg;
             tr[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=marcz;
             tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=margb;
             tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=marunit;
             tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=marfzunit;
         }
         
    }
    
    
    function providerchanged(i)
    {
      var provider;
      var providerid;      var providernm;      var rank;      table=document.getElementById("tab1");      tr=table.getElementsByTagName("tr");
            provider=tr[1].getElementsByTagName("td")[i].getElementsByTagName("input")[0].value.Trim();
            if(provider.indexOf("|")>0)
            {
                var strs = provider.split("|");
                if(strs.length==3)
                {
                   tr[1].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value=provider;
                }
            }
            else if(provider=="")
            {
                tr[1].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value="";
            }
            if(tr[1].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value=="")
            {
               providerid="";
               providernm="";
               rank="";
            }
            else
            {
               var strs2 = tr[1].getElementsByTagName("td")[i].getElementsByTagName("input")[2].value.split("|");
               providerid=strs2[0];
               providernm=strs2[1];
               rank=strs2[2];
            }
            tr[1].getElementsByTagName("td")[i].getElementsByTagName("input")[1].value=providerid;
            tr[1].getElementsByTagName("td")[i].getElementsByTagName("input")[0].value=providernm;
            tr[1].getElementsByTagName("td")[i].getElementsByTagName("span")[0].innerHTML=rank;
    }
       function setHiddenCol()
        {    
            //document.getElementById("ctl00_PrimaryContent_TextBox1").value=document.getElementById("ctl00_PrimaryContent_TextBox1").value=="1"?document.getElementById("ctl00_PrimaryContent_TextBox1").value="0":document.getElementById("ctl00_PrimaryContent_TextBox1").value="1";
            document.getElementById("hid").value=document.getElementById("hid").value=="1"?document.getElementById("hid").value="0":document.getElementById("hid").value="1";
            hiddenprovcol()
        }    
        function setHiddenRow()
        {    
             oTable.rows[iRow].style.display = oTable.rows[iRow].style.display == "none"?"block":"none";    
        } 
        
        function hiddencol()
        {
           var oTable=document.getElementById('tab');
            if(document.getElementById("hid").value=="1")//显示
            {
                 for(i=3;i < oTable.rows.length-2; i++)    
                 {  
                       for(j=0;j<6;j++)//六个供应商
                       {
                            oTable.rows[i].cells[15+j*3].style.display ="block";  
                            oTable.rows[i].cells[16+j*3].style.display = "block";  
                            oTable.rows[i].cells[17+j*3].colSpan="1";
                       }
                  }
                  for(j=0;j<6;j++)//六个供应商
                  {
                      oTable.rows[oTable.rows.length-2].cells[7+j*3].style.display = "block";  
                      oTable.rows[oTable.rows.length-2].cells[8+j*3].style.display = "block";  
                      oTable.rows[oTable.rows.length-2].cells[9+j*3].colSpan="1"; 
                  }
            }
            else//隐藏
            {
                for(i=3;i < oTable.rows.length-2; i++)    
                 {  
                       for(j=0;j<6;j++)//六个供应商
                       {
                            oTable.rows[i].cells[15+j*3].style.display ="none";  
                            oTable.rows[i].cells[16+j*3].style.display = "none";  
                            oTable.rows[i].cells[17+j*3].colSpan="3";
                       }
                  }
                  for(j=0;j<6;j++)//六个供应商
                  {
                      oTable.rows[oTable.rows.length-2].cells[7+j*3].style.display = "none";  
                      oTable.rows[oTable.rows.length-2].cells[8+j*3].style.display = "none";  
                      oTable.rows[oTable.rows.length-2].cells[9+j*3].colSpan="3"; 
                  }
              }
        }  
        function hiddenprovcol()
          {
              
              hiddencol();
              var oTable=document.getElementById('tab');
              //document.getElementById("ctl00_PrimaryContent_TextBox1").value=parseInt(document.getElementById("ctl00_PrimaryContent_TextBox1").value)+1;
              var num=document.getElementById("ctl00_PrimaryContent_TextBox1").value;
              //第一行
              oTable.rows[0].cells[2].colSpan=3*num;
              //第二行、第三行
              for(j=num;j<6;j++)
              {
                  oTable.rows[1].cells[j].style.display ="none";  
                  oTable.rows[2].cells[j].style.display ="none";  
              }
              //表体
              for(i=3;i < oTable.rows.length-2; i++)    
              {
                  for(j=num;j<6;j++)
                  {
                      oTable.rows[i].cells[15+j*3].style.display ="none";  
                      oTable.rows[i].cells[16+j*3].style.display = "none";  
                      oTable.rows[i].cells[17+j*3].style.display = "none";  
                  }
              }
              for(j=num;j<6;j++)
              {
                  oTable.rows[oTable.rows.length-2].cells[7+j*3].style.display ="none";  
                  oTable.rows[oTable.rows.length-2].cells[8+j*3].style.display ="none";  
                  oTable.rows[oTable.rows.length-2].cells[9+j*3].style.display ="none";  
              }
          } 
          //**********隐藏重复行数据***********************
          function hiddenrow(startrownum,endrownum,col)
          {
                table=document.getElementById("tab");                tr=table.getElementsByTagName("tr");
                var coltext;
                var coltextbef;
                for(i=2;i <tr.length-1;i++) 
                { 
                    coltextbef=tr[i-1].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                    coltext=tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                    if(coltext==coltextbef)
                    {
                          for(j=startrownum;j<=endrownum;j++)
                          {
                               tr[i].getElementsByTagName("td")[j].getElementsByTagName("span")[0].style.display ="none";  
                          }
                    }
                }
          }