<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_CanBuYDADD.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CanBuYDADD"
    Title="餐补异动" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    餐补异动
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }

        function GetBF(obj){ 
        
          if(isNaN(parseFloat(obj.value)))
           {
           alert("请输入正确的数量!");
           obj.value=0;
           return; 
           }
//          obj.value=parseInt(tzts)*(cbbz);
           var tr=obj.parentNode.parentNode;
           var obj_tzts=tr.getElementsByTagName("td")[4].getElementsByTagName("input")[0];
           var obj_cbbz=tr.getElementsByTagName("td")[5].getElementsByTagName("input")[0];

           if(obj_tzts.value!=""&&obj_cbbz.value!="")
           {  
             tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=(obj_tzts.value)*(obj_cbbz.value);
           }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
   
        <ContentTemplate>
            <div style="width: 100%">
                <table width="100%">
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;行数：
                            <asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" />
                        </td>
                        <td align="left">                            
                            <asp:FileUpload ID="FileUpload" runat="server" />&nbsp;&nbsp;
                            <asp:Button ID="btnImprot" Text="导入" runat="server" OnClick="btnImprot_Click" />
                        </td>
                        <td align="right">
                            年月：
                            <asp:TextBox ID="tb_yearmonth" Width="90px" runat="server"></asp:TextBox>
                            <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                                ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                            </asp:CalendarExtender>
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="btnsave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div>
                <hr />
            </div>
            <div style="text-align: center; overflow: auto; height: 400px">
                <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <asp:Repeater ID="Det_Repeater" runat="server">
                        <HeaderTemplate>
                            <tr class="tableTitle headcolor">
                                <td>
                                    序号
                                </td>
                                <td>
                                    姓名
                                </td>
                                <td>
                                    工号
                                </td>
                                <td>
                                    部门
                                </td>
                                <td>
                                    调整天数
                                </td>
                                <td>
                                    餐补标准
                                </td>
                                <td>
                                    补发
                                </td>
                                <td>
                                    备注
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
                                    <div style="width: 50px">
                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                        <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                        </asp:CheckBox></div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt1" runat="server" OnTextChanged="Textname_TextChanged" Text='<%#Eval("cbyd_name")%>'
                                        AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                                        ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txt1" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                    <asp:TextBox ID="txt2" runat="server" Text='<%#Eval("cbyd_stid")%>' Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt3" runat="server" Width="100px" Text='<%#Eval("cbyd_gh") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt4" runat="server" Width="100px" Text='<%#Eval("cbyd_bm") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt5" runat="server" Width="100px" onkeyup="GetBF(this)" Text='<%#Eval("cbyd_tzts") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt6" runat="server" Width="100px" onkeyup="GetBF(this)" Text='<%#Eval("cbyd_cbbz") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt7" runat="server" Width="100px" Text='<%#Eval("cbyd_bf") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt8" runat="server" Width="150px" Text='<%#Eval("cbyd_note") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
            </div>
            <div style="float: left; padding-top: 10px">
                &nbsp; &nbsp; &nbsp;<asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click"
                    Visible="false" /></div>
            <br />
            <br />
            <br />
        </ContentTemplate>
</asp:Content>
