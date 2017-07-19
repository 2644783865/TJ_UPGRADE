<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_GsBaseDetail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.TM_GsBaseDetail" Title="工时基础数据明细" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
工时基础数据明细
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
             $("#tuhaoinfo tr:eq(3) input:eq(2)").val(pertuhaotolgs.toFixed(2));
          }
          
          
          
          //零件数量变化计算和拆分件总工时变化计算
          function stastics2(obj)
          {
             var checkcfjtolgs=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.trim();
             if(isNaN(checkcfjtolgs)==true)
             {
                obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
             }
             
             var pertuhaotolgs=0;
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
             $("#tuhaoinfo tr:eq(3) input:eq(2)").val(pertuhaotolgs.toFixed(2));
          }
          
          
          //单件工时输入检查
//          function checkpergsnum(obj)
//          {
//             var bjpergs=obj.parentNode.parentNode.getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.trim();
//             if(isNaN(bjpergs)==true)
//             {
//                obj.parentNode.parentNode.getElementsByTagName("td")[5].getElementsByTagName("input")[0].value="0";
//             }
//          }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
            <div style="width: 100%">
                  <table width="100%">
                    <tr>
                        <td align="right">
                            <a id="btnsave" class="easyui-linkbutton" runat="server" onserverclick="btnsave_click">保存</a>
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
                                <asp:Label ID="lbzdper" runat="server"></asp:Label>
                                <asp:Label ID="lbzdperid" runat="server" Visible="false"></asp:Label>
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
                                是否在用：
                            </td>
                            <td align="center">
                                <asp:RadioButtonList ID="rad_state" runat="server" RepeatColumns="2">
                                    <asp:ListItem Text="在用" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="停用" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
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
                            <td align="center">
                                总图号：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtzongmap" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
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
                                <asp:TextBox ID="txtbjmap" runat="server"></asp:TextBox>
                                <asp:Label ID="btx1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="center">
                                部件单件工时：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtbjpergs" runat="server" onfocus="this.blur()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                编号：
                            </td>
                            <td align="center">
                                <asp:Label ID="txtcontext" runat="server"></asp:Label>
                            </td>
                            <td>
                                备注：
                            </td>
                            <td align="center" colspan="3">
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
                        </td>
                        <td align="right">
                            <asp:Button ID="delete" runat="server" Text="移除行" OnClick="delete_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                零件数量
                            </td>
                            <td align="center">
                                拆分件图中序号
                            </td>
                            <td align="center">
                                拆分件名称及规格
                            </td>
                            <td align="center">
                                拆分件数量
                            </td>
                            <td align="center">
                                工序
                            </td>
                            <td align="center">
                                单件工时
                            </td>
                            <td align="center">
                                拆分件合计工时
                            </td>
                            <td align="center">
                                备注
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                </asp:CheckBox>
                                <asp:HiddenField ID="mxid" runat="server" Value='<%#Eval("mxid")%>' />
                            </td>
                            <td>
                                <asp:TextBox ID="ljmap" runat="server" Text='<%#Eval("ljmap")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="ljname" runat="server" Width="100px"
                                    Text='<%#Eval("ljname") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="ljnum" runat="server" Width="100px" name="num" 
                                    Text='<%#Eval("ljnum") %>' onkeyup="stastics2(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfjmapbh" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfjmapbh") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfjname" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfjname") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfjnum" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfjnum") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="gxdetal" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("gxdetal") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfjpergs" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfjpergs") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="cfjtolgs" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("cfjtolgs") %>' onkeyup="stastics2(this)" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="notedetail" runat="server" Width="100px" name="price"
                                    Text='<%#Eval("notedetail") %>' ToolTip='<%#Eval("notedetail") %>' onkeyup="calculate(this)" autocomplete="off"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
              </table>
            </div>
</asp:Content>
