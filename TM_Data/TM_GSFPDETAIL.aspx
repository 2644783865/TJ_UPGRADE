<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_GSFPDETAIL.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_GSFPDETAIL" Title="工时分配明细" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
  工时分配明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
         window.onload=function(){
           var tab = document.getElementById("tab");
           var lingjiantuhao="";
           for (i = 1; i < tab.rows.length; i++)
           {
                if(tab.rows[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value.trim()!=""&&tab.rows[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value.trim()!=lingjiantuhao)
                {
                   lingjiantuhao=tab.rows[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value.trim();
                }
                else
                {
                    for(var m = 1; m < 3; m++)
                    {
                        tab.rows[i].getElementsByTagName("td")[m].getElementsByTagName("input")[0].value="";
                    }
                }
            }
        }
        
        
        //检查必填项
        function checkbtx()
        {
            var tuhaoinfo=document.getElementById("tuhaoinfo");
            var jcbh=tuhaoinfo.rows[1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim();
            var yearmonth=tuhaoinfo.rows[2].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value.trim();
            var bjmap=tuhaoinfo.rows[3].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.trim();
            if(jcbh==""||yearmonth==""||bjmap=="")
            {
               alert("有必填项未填写！！！");
            }
        }
    
    
         function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
          }
          
          //计算和检查
          function calculate(obj)
          {
             //判断输入是否为数值
             var ljnum=obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim();
             var cfjnum=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value.trim();
             var cfjpergs=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.trim();
             var cfjtolgs=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim();
             if(isNaN(ljnum)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("input")[0].value="0";
             }
             if(isNaN(cfjnum)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value="0";
             }
             if(isNaN(cfjpergs)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
             }
             if(isNaN(cfjtolgs)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
             }
             
             //重新赋值
             ljnum=obj.parentNode.parentNode.getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim();
             cfjnum=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value.trim();
             cfjpergs=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.trim();
             cfjtolgs=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim();
             //给该行的零件数量赋值
             var tab = document.getElementById("tab");
             var ljmap=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value.trim();
             var rowindex=obj.parentNode.parentNode.rowIndex;
             if(ljmap=="")
             {
                if(rowindex>=2&&tab.rows[rowindex].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim()=="")
                {
                   tab.rows[rowindex].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=tab.rows[rowindex-1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim();
                }
             }
             //计算当前行的拆分件工时合计
             if(cfjnum!=""&&cfjpergs!="")
             {
                var cfjnumjs=parseFloat(cfjnum);
                var cfjpergsjs=parseFloat(cfjpergs);
                var cfjtolgsjs=cfjnumjs*cfjpergsjs;
                obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=cfjtolgsjs.toFixed(2);
             }
             //统计工时总数
             stastics1();
          }
          //合计统计
          function stastics1()
          {
             var pertuhaotolgs=0;
             var bjtolgs=0;
             var tab = document.getElementById("tab");
             for (i = 1; i < tab.rows.length; i++)
             {
               var cfjtolgs="";
               var ljnum="";
               if(tab.rows[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim()!=""&&tab.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim()!="")
               {
                  cfjtolgs=tab.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim();
                  ljnum=tab.rows[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim();
                  var cfjtolgsjs=parseFloat(cfjtolgs);
                  var ljnumjs=parseFloat(ljnum);
                  pertuhaotolgs+=cfjtolgsjs*ljnumjs;
               }
             }
             
             var bjnum=$("#tuhaoinfo tr:eq(5) input:eq(1)").val();
             if(bjnum!="")
             {
                var bjnumjs=parseFloat(bjnum);
                bjtolgs=pertuhaotolgs*bjnumjs;
             }
             $("#tuhaoinfo tr:eq(5) input:eq(0)").val(pertuhaotolgs.toFixed(2));
             $("#tuhaoinfo tr:eq(5) input:eq(2)").val(bjtolgs.toFixed(2));
          }
          
          
          
          //零件数量和拆分件总工时变化计算
          function stastics2(obj)
          {
             var checkcfjtolgs=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim();
             if(isNaN(checkcfjtolgs)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
             }
             
             var pertuhaotolgs=0;
             var bjtolgs=0;
             var tab = document.getElementById("tab");
             for (i = 1; i < tab.rows.length; i++)
             {
               var cfjtolgs="";
               var ljnum="";
               if(tab.rows[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim()!=""&&tab.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim()!="")
               {
                  cfjtolgs=tab.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim();
                  ljnum=tab.rows[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.trim();
                  var cfjtolgsjs=parseFloat(cfjtolgs);
                  var ljnumjs=parseFloat(ljnum);
                  pertuhaotolgs+=cfjtolgsjs*ljnumjs;
               }
             }
             var bjnum=$("#tuhaoinfo tr:eq(5) input:eq(1)").val();
             if(bjnum!="")
             {
                var bjnumjs=parseFloat(bjnum);
                bjtolgs=pertuhaotolgs*bjnumjs;
             }
             $("#tuhaoinfo tr:eq(5) input:eq(0)").val(pertuhaotolgs.toFixed(2));
             $("#tuhaoinfo tr:eq(5) input:eq(2)").val(bjtolgs.toFixed(2));
          }
          
          
          
          //结算数据计算
          function account(obj)
          {
             //判断输入是否为数值
             var ljnumaccount=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.trim();
             var cfjnumaccount=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.trim();
             var cfjpergsaccount=obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.trim();
             var cfjtolgsaccount=obj.parentNode.parentNode.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.trim();
             if(isNaN(ljnumaccount)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="0";
             }
             if(isNaN(cfjnumaccount)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="0";
             }
             if(isNaN(cfjpergsaccount)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value="0";
             }
             if(isNaN(cfjtolgsaccount)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
             }
             
             //重新赋值
             ljnumaccount=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.trim();
             cfjnumaccount=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.trim();
             cfjpergsaccount=obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.trim();
             cfjtolgsaccount=obj.parentNode.parentNode.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.trim();
             
             
             //计算当前行的拆分件工时合计
             if(cfjnumaccount!=""&&cfjpergsaccount!="")
             {
                var cfjnumjsaccount=parseFloat(cfjnumaccount);
                var cfjpergsjsaccount=parseFloat(cfjpergsaccount);
                var cfjtolgsjsaccount=cfjnumjsaccount*cfjpergsjsaccount;
                obj.parentNode.parentNode.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=cfjtolgsjsaccount.toFixed(2);
             }
             //统计工时总数
             var pertuhaotolgsaccount=0;
             var bjtolgsaccount=0;
             var tab = document.getElementById("tab");
             for (i = 1; i < tab.rows.length; i++)
             {
               var cfjtolgsaccount="";
               var ljnumaccount="";
               if(tab.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.trim()!=""&&tab.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim()!="")
               {
                  cfjtolgsaccount=tab.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.trim();
                  ljnumaccount=tab.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.trim();
                  var cfjtolgsjsaccount=parseFloat(cfjtolgsaccount);
                  var ljnumjsaccount=parseFloat(ljnumaccount);
                  pertuhaotolgsaccount+=cfjtolgsjsaccount*ljnumjsaccount;
               }
             }
             var bjnumaccount=$("#tuhaoinfo tr:eq(6) input:eq(1)").val();
             if(bjnumaccount!="")
             {
                var bjnumjsaccount=parseFloat(bjnumaccount);
                bjtolgsaccount=pertuhaotolgsaccount*bjnumjsaccount;
             }
             $("#tuhaoinfo tr:eq(6) input:eq(0)").val(pertuhaotolgsaccount.toFixed(2));
             $("#tuhaoinfo tr:eq(6) input:eq(2)").val(bjtolgsaccount.toFixed(2));
          }
          
          //零件数量和拆分件总工时变化计算
          function accounttol(obj)
          {
             var checkcfjtolgsaccount=obj.parentNode.parentNode.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.trim();
             if(isNaN(checkcfjtolgsaccount)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
             }
             
             
             var pertuhaotolgsaccount=0;
             var bjtolgsaccount=0;
             var tab = document.getElementById("tab");
             for (i = 1; i < tab.rows.length; i++)
             {
               var cfjtolgsaccount="";
               var ljnumaccount="";
               if(tab.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.trim()!=""&&tab.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim()!="")
               {
                  cfjtolgsaccount=tab.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.trim();
                  ljnumaccount=tab.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.trim();
                  var cfjtolgsjsaccount=parseFloat(cfjtolgsaccount);
                  var ljnumjsaccount=parseFloat(ljnumaccount);
                  pertuhaotolgsaccount+=cfjtolgsjsaccount*ljnumjsaccount;
               }
             }
             var bjnumaccount=$("#tuhaoinfo tr:eq(6) input:eq(1)").val();
             if(bjnumaccount!="")
             {
                var bjnumjsaccount=parseFloat(bjnumaccount);
                bjtolgsaccount=pertuhaotolgsaccount*bjnumjsaccount;
             }
             $("#tuhaoinfo tr:eq(6) input:eq(0)").val(pertuhaotolgsaccount.toFixed(2));
             $("#tuhaoinfo tr:eq(6) input:eq(2)").val(bjtolgsaccount.toFixed(2));
          }
          
          
          //定额总工时计算
          function accountbjgs_de(obj)
          {
             var bjpergs=$("#tuhaoinfo tr:eq(5) input:eq(0)").val();
             var bjnum=$("#tuhaoinfo tr:eq(5) input:eq(1)").val();
             if(isNaN(bjpergs)==true)
             {
                $("#tuhaoinfo tr:eq(5) input:eq(0)").val("0");
             }
             if(isNaN(bjnum)==true)
             {
                $("#tuhaoinfo tr:eq(5) input:eq(1)").val("0");
             }
             bjpergs=$("#tuhaoinfo tr:eq(5) input:eq(0)").val();
             bjnum=$("#tuhaoinfo tr:eq(5) input:eq(1)").val();
             if(bjpergs!=""&&bjnum!="")
             {
                var bjpergsjs=parseFloat(bjpergs);
                var bjnumjs=parseFloat(bjnum);
                var bjtolgs=bjpergsjs*bjnumjs;
                $("#tuhaoinfo tr:eq(5) input:eq(2)").val(bjtolgs.toFixed(2));
             }
          }
          
          
          //结算总工时计算
          function accountbjgs_js(obj)
          {
             var bjpergsaccount=$("#tuhaoinfo tr:eq(6) input:eq(0)").val();
             var bjnumaccount=$("#tuhaoinfo tr:eq(6) input:eq(1)").val();
             if(isNaN(bjpergsaccount)==true)
             {
                $("#tuhaoinfo tr:eq(6) input:eq(0)").val("0");
             }
             if(isNaN(bjnumaccount)==true)
             {
                $("#tuhaoinfo tr:eq(6) input:eq(1)").val("0");
             }
             bjpergsaccount=$("#tuhaoinfo tr:eq(6) input:eq(0)").val();
             bjnumaccount=$("#tuhaoinfo tr:eq(6) input:eq(1)").val();
             if(bjpergsaccount!=""&&bjnumaccount!="")
             {
                var bjpergsjsaccount=parseFloat(bjpergsaccount);
                var bjnumjsaccount=parseFloat(bjnumaccount);
                var bjtolgsaccount=bjpergsjsaccount*bjnumjsaccount;
                $("#tuhaoinfo tr:eq(6) input:eq(2)").val(bjtolgsaccount.toFixed(2));
             }
          }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
            <div style="width: 100%">
                  <table width="100%">
                    <tr>
                        <td align="right">
                            <a id="btnsave" class="easyui-linkbutton" runat="server" onclick="return checkbtx()" onserverclick="btnsave_click">保存</a>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <a id="btnaccount" class="easyui-linkbutton" runat="server" onclick="return confirm('结算后将不能修改，确认结算吗?')" onserverclick="btnaccount_click">确认结算</a>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                  </table>
             </div>
             <div>
                 <table id="tuhaoinfo" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <tr>
                            <td align="center">
                                制定人：
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzdrname" runat="server"></asp:Label>
                                <asp:Label ID="lbzdrid" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbusername" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbuserid" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td align="center">
                                制定时间：
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzdtime" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                结算状态：
                            </td>
                            <td align="center">
                                <asp:RadioButtonList ID="rad_state" runat="server" RepeatColumns="2" Enabled="false">
                                    <asp:ListItem Text="已结算" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="未结算" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                单据编号：
                            </td>
                            <td align="center">
                                <asp:Label ID="lbdjbh" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                机床编号：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtjcbh" runat="server" AutoPostBack="True" OnTextChanged="txtjcbh_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="jcbh_AutoCompleteExtender" runat="server" 
                                    DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                    MinimumPrefixLength="1" ServiceMethod="get_mecinfo" ServicePath="~/Ajax.asmx" 
                                    TargetControlID="txtjcbh" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:Label ID="lbbtx1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="center">
                                机床类型：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtjctype" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                年月：
                            </td>
                            <td align="center">
                                <input type="text" style="width:80px" id="yearmonth" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;},editable:false" runat="server" class="easyui-datebox" />
                                <asp:Label ID="lbbtx2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="center">
                                产品名称：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtcpname" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                产品规格：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtcpguige" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                总图号：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtzongmap" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                部件名称：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtbjname" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                部件图号：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtbjmap" runat="server" AutoPostBack="True" OnTextChanged="txtbjmap_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                    DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                    MinimumPrefixLength="1" ServiceMethod="get_bjinfo" ServicePath="~/Ajax.asmx" 
                                    TargetControlID="txtbjmap" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                单件工时(定额)：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtbjpergs" runat="server" onkeyup="accountbjgs_de(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td align="center">
                                数量(定额)：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtbjnum" runat="server" onkeyup="accountbjgs_de(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td align="center">
                                总工时(定额)：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtbjtolgs" runat="server" autocomplete="off"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                单件工时(结算)：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtrealbjpergs" runat="server" onkeyup="accountbjgs_js(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td align="center">
                                数量(结算)：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtrealbjnum" runat="server" onkeyup="accountbjgs_js(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td align="center">
                                总工时(结算)：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtrealbjtotalgs" runat="server" autocomplete="off"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                结算人：
                            </td>
                            <td align="center">
                                <asp:Label ID="jsrname" runat="server"></asp:Label>
                                <asp:Label ID="jsrid" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="jsusername" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbjsuserid" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td align="center">
                                结算时间：
                            </td>
                            <td align="center">
                               <asp:Label ID="lbjstime" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td align="center">
                               人员信息：
                            </td>
                            <td align="center">
                               <asp:TextBox ID="txtperinfo" Width="120px" ToolTip="this.text" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                备注：
                            </td>
                            <td align="center" colspan="5">
                                <asp:TextBox ID="txtnote" runat="server" Width="90%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
              </div>
              
              <div>
                <table width="100%">
                   <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lbtitle" runat="server" Text="部件图号工时明细"></asp:Label>
                        </td>
                   </tr>
                   <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;行数：
                            <asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnadd" runat="server" Text="增 加" OnClick="btnadd_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="delete" runat="server" Text="移除行" OnClick="delete_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            要加载的部件图号：<asp:TextBox ID="txtbjmapload" runat="server" AutoPostBack="True" OnTextChanged="txtbjmapload_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;工序：<asp:TextBox ID="txtgx" Width="50px" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                MinimumPrefixLength="1" ServiceMethod="get_bjinfo" ServicePath="~/Ajax.asmx" 
                                TargetControlID="txtbjmapload" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                            <a id="btnloadbase" class="easyui-linkbutton" runat="server" onserverclick="btnloadbase_click">加载基础数据</a>
                            <asp:Label ID="lbbtx3" runat="server" Text="*多个图号以英文逗号隔开" ForeColor="Red"></asp:Label>
                        </td>
                   </tr>
                </table>
                <table id="tab" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                  <asp:Repeater ID="Det_Repeater" runat="server">
                    <HeaderTemplate>
                         <tr class="tableTitle headcolor">
                            <td align="center">
                                序号
                            </td>
                            <td align="center">
                                零件图号
                            </td>
                            <td align="center">
                                零件名称
                            </td>
                            <td align="center">
                                零件数量(定额)
                            </td>
                            <td align="center">
                                拆分件图中序号
                            </td>
                            <td align="center">
                                拆分件名称及规格
                            </td>
                            <td align="center">
                                拆分件数量(定额)
                            </td>
                            <td align="center">
                                工序
                            </td>
                            <td align="center">
                                单件工时(定额)
                            </td>
                            <td align="center">
                                拆分件合计工时(定额)
                            </td>
                            <td align="center">
                                备注(定额)
                            </td>
                            <td align="center">
                                零件数量(结算)
                            </td>
                            <td align="center">
                                拆分件数量(结算)
                            </td>
                            <td align="center">
                                单件工时(结算)
                            </td>
                            <td align="center">
                                拆分件合计工时(结算)
                            </td>
                            <td align="center">
                                备注(结算)
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                </asp:CheckBox>
                                <asp:HiddenField ID="detailid" runat="server" Value='<%#Eval("detailid")%>' />
                            </td>
                            <td>
                                <asp:TextBox ID="gs_ljmap" runat="server" Text='<%#Eval("gs_ljmap")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="gs_ljname" runat="server" Width="100px"
                                    Text='<%#Eval("gs_ljname") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="gs_ljnum" runat="server" Width="100px" name="num" 
                                    Text='<%#Eval("gs_ljnum") %>' onkeyup="stastics2(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfj_mapbh" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfj_mapbh") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfj_name" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfj_name") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfj_num" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfj_num") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="gs_gxdetal" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("gs_gxdetal") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="gs_cfjpergs" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("gs_cfjpergs") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="gs_cfjtolgs" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("gs_cfjtolgs") %>' onkeyup="stastics2(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="gs_notedetail" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("gs_notedetail") %>' ToolTip='<%#Eval("gs_notedetail") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            
                            
                            <td>
                                <asp:TextBox ID="realljnum" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("realljnum") %>' onkeyup="accounttol(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="realcfjnum" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("realcfjnum") %>' onkeyup="account(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="realcfjpergs" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("realcfjpergs") %>' onkeyup="account(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="realcfjtolgs" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("realcfjtolgs") %>' onkeyup="accounttol(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="realnote" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("realnote") %>' ToolTip='<%#Eval("realnote") %>'></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
              </table>
            </div>
</asp:Content>
