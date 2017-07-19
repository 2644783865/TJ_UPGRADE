<%@ Page Language="C#"  MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="OM_GDGZAdd.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GDGZAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定工资修改 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
     <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var i;
        array = new Array();
        function SelPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }

        function SelPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelPersons3() {
            $("#hidPerson").val("third");
            SelPersons();
        }
        
   
        
        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            else if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>
    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
        function sum(obj){
            var edu=obj.parentNode.getElementsByTagName("input")[0].value.trim();
            var gdgznow=0;
            var lastgdgz=obj.parentNode.parentNode.getElementsByTagName("td")[2].getElementsByTagName("input")[0].value.trim();
            if(lastgdgz!=""&edu!="")
            {
                gdgznow=parseFloat(lastgdgz)+parseFloat(edu);
                obj.parentNode.parentNode.getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=gdgznow;
            }
        }
    </script>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    
    </asp:ToolkitScriptManager>
    
        <div>
             <table width="100%">
                 <tr>
                     <td align="left">
                            &nbsp;&nbsp;&nbsp;行数：
                            <asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" />
                    </td>
                     <td align="right">
                           <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" BorderWidth="2px" Font-Bold="true" Visible="false" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button runat="server" ID="btnaudit" Text="提交" OnClick="btnaudit_OnClick" Visible="false" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     </td>
                 </tr>
             </table>
        </div>
    
     <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" 
         ActiveTabIndex="0" >
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="数据明细" TabIndex="0">
            <ContentTemplate>
                  
                  <div class="box-wrapper1">
                     <div class="box-outer" style="text-align: left">
                             <table id="table1" align="center" cellpadding="2" cellspacing="1" border="1">
                               <asp:Repeater ID="rptProNumCost" runat="server">
                                     <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;white-space:nowrap;">
                                            <td style="border:solid 1px" align="center">
                                                姓名
                                            </td>
                                            <td style="border:solid 1px" align="center">
                                                部门
                                            </td>
                                            <td style="border:solid 1px" align="center">
                                                调整前固定工资
                                            </td>
                                            <td style="border:solid 1px" align="center">
                                                调整额度
                                            </td>
                                            <td style="border:solid 1px" align="center">
                                                固定工资
                                            </td>
                                            <td style="border:solid 1px" align="center">
                                                工号
                                            </td>
                                            <td style="border:solid 1px" align="center">
                                                备注
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" style="white-space:nowrap;">
                                        <td style="border:solid 1px">
                                            <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                            <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                            </asp:CheckBox>
                                            <asp:TextBox ID="txt_name" Text='<%#Eval("ST_NAME")%>' runat="server" AutoPostBack="True" 
                                                onclick="this.select();" OnTextChanged="Textname_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" 
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                                MinimumPrefixLength="1" ServiceMethod="get_st_id" ServicePath="~/Ajax.asmx" 
                                                TargetControlID="txt_name" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                            <asp:TextBox ID="txtstid" runat="server" Text='<%#Eval("ST_ID")%>' Visible="False"></asp:TextBox>
                                        </td>
                                        <td  style="border:solid 1px">
                                            </strong><asp:TextBox ID="txtbm" Text='<%#Eval("DEP_NAME")%>' runat="server"  onfocus="this.blur()"></asp:TextBox>
                                        </td>
                                        <td style="border:solid 1px">
                                            <asp:TextBox ID="txt_lastgdgz" Text='<%#Eval("lastgdgz")%>' runat="server" onfocus="this.blur()"></asp:TextBox>
                                            
                                        </td>
                                        <td style="border:solid 1px">
                                            <asp:TextBox ID="txt_tzedu" Text='<%#Eval("tzedu")%>' runat="server"  onkeyup="sum(this)"></asp:TextBox>
                                            
                                        </td>
                                        <td style="border:solid 1px">
                                            <asp:TextBox ID="txt_gdgz" Text='<%#Eval("GDGZ")%>'  runat="server"></asp:TextBox>
                                        </td>
                                        <td style="border:solid 1px">
                                            <asp:TextBox ID="txt_gh" runat="server" Text='<%#Eval("Person_GH")%>'  onfocus="this.blur()"></asp:TextBox>
                                            
                                        </td>
                                        <td style="border:solid 1px">
                                            <asp:TextBox ID="txt_note" runat="server" Text='<%#Eval("NOTE")%>' Height="30px" 
                                                TextMode="MultiLine" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                         </table>
                         <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
                    </div>
                </div>
                <div style="float: left; padding-top: 10px">
                &nbsp; &nbsp; &nbsp;<asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click"/></div>
                    <br />
                    <br />
                    <br /> 
              </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="审核" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper">
                  <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblSHJS_OnSelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    固定工资审批
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center">
                                    制单人
                                </td>
                                <td>
                                    <asp:Label ID="lbzdr" runat="server" Width="100%"></asp:Label>
                                </td>
                                <td align="center">
                                    制单时间
                                </td>
                                <td>
                                    <asp:Label ID="lbzdtime" runat="server" Width="40%" />
                                </td>
                            </tr>
                            <tr id="yjshh" runat="server" visible="false">
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelPersons1()" Visible="false">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinion1" runat="server" Height="42px" TextMode="MultiLine"
                                                    Width="100%" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="ejshh" runat="server" visible="false">
                                <td align="center">
                                    二级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelPersons2()" Visible="false">
                                                    <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinion2" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            <tr id="sjshh" runat="server" visible="false">
                                <td align="center">
                                    三级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelPersons3()" Visible="false">
                                                    <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinion3" runat="server" TextMode="MultiLine" Width="100%"
                                                Height="42px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer> 
    
    <div>
        <div>
            <div id="win" visible="false">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                按部门查询：
                            </td>
                            <td>
                                <input id="dep" name="dept"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 430px; height: 230px">
                    <table id="dg">
                    </table>
                </div>
            </div>
            <div id="buttons" style="text-align: right" visible="false">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>       
</asp:Content>
