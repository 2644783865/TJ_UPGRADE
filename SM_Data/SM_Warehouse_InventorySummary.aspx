<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Warehouse_InventorySummary.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_InventorySummary" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript" language="javascript">
    function exporttip() {
        var retVal = confirm("导出前请先保存，确定导出？");
        return retVal;
    }

    function closewin() {
        var retVal = confirm("返回前请先保存，确定返回？");
        if (retVal == true) {
            window.close();
        }
        else {
            return;
        }
    }
    
</script>

    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>
            </td>
            <td align="right">
            <asp:Button ID="ExportFile" runat="server" Text="导出" OnClick="ExportFile_Click" OnClientClick="return exporttip()" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" />&nbsp;&nbsp;&nbsp;
            <input id="Close" type="button" value="关闭" onclick="closewin()" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    
    <asp:Panel ID="PanelBody" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    <table id="rp1" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td rowspan="3"><strong>序号</strong></td>
                <td rowspan="3"><strong>存货编码</strong></td>
                <td rowspan="2" colspan="2"><strong>存货名称和规格</strong></td>
                <td rowspan="3"><strong>单位</strong></td>
                <td rowspan="3"><strong>单价</strong></td>
                <td rowspan="2" colspan="2"><strong>盘点前仓库台账账面记录</strong></td>
                <td colspan="4"><strong>尚未入账数量</strong></td>
                <td rowspan="2" colspan="2"><strong>应结存</strong></td>
                <td rowspan="2" colspan="2"><strong>盘点记录</strong></td>
                <td rowspan="2" colspan="2"><strong>差异记录</strong></td>
                <td rowspan="3"><strong>差异原因</strong></td>
                <td rowspan="3"><strong>备注(拟报废额)</strong></td>                            
            </tr>
            <tr>
                <td colspan="2"><strong>入库</strong></td>
                <td colspan="2"><strong>发出</strong></td> 
            </tr>
            <tr>
                <td><strong>存货名称</strong></td>
                <td><strong>存货规格</strong></td>
                <td><strong>数量</strong></td>
                <td><strong>金额</strong></td>
                <td><strong>数量</strong></td>
                <td><strong>金额</strong></td>
                <td><strong>数量</strong></td>
                <td><strong>金额</strong></td>                   
                <td><strong>数量</strong></td>
                <td><strong>金额</strong></td>                   
                <td><strong>数量</strong></td>
                <td><strong>金额</strong></td>
                <td><strong>数量</strong></td>
                <td><strong>金额</strong></td>                                                     
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label></td>                
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label></td>
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>'></asp:Label></td>
                <td><asp:Label ID="LabelNumInAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumInAccount")%>'></asp:Label></td>
                <td><asp:Label ID="LabelAmountInAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountInAccount")%>'></asp:Label></td>
                <td><asp:Label ID="LabelNumNotIn" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container.DataItem, "NumNotIn")%>'></asp:Label></td>
                <td>
                    <%--<input type="text" id="InputAmountNotIn" value='<%#DataBinder.Eval(Container.DataItem, "AmountNotIn")%>' readonly="readonly" runat="server" style="width:100px;"/>--%>
                    <asp:Label ID="LabelAmountNotIn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountNotIn")%>'></asp:Label>
                </td>
                <td><asp:Label ID="LabelNumNotOut" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container.DataItem, "NumNotOut")%>'></asp:Label></td>
                <td>
                    <%--<input type="text" id="InputAmountNotOut" value='<%#DataBinder.Eval(Container.DataItem, "AmountNotOut")%>' readonly="readonly" runat="server" style="width:100px;"/>--%>
                    <asp:Label ID="LabelAmountNotOut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountNotOut")%>'></asp:Label>
                </td>
                <td>
                    <%--<input type="text" id="InputNumDueToAccount" value='<%#DataBinder.Eval(Container.DataItem, "NumDueToAccount")%>' readonly="readonly" runat="server" style="width:100px;"/>--%>
                    <asp:Label ID="LabelNumDueToAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumDueToAccount")%>'></asp:Label>
                </td>
                <td>
                    <%--<input type="text" id="InputAmountDueToAccount" value='<%#DataBinder.Eval(Container.DataItem, "AmountDueToAccount")%>' readonly="readonly" runat="server" style="width:100px;"/>--%>
                    <asp:Label ID="LabelAmountDueToAccount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountDueToAccount")%>'></asp:Label>
                </td>
                <td><asp:Label ID="LabelNumInventory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumInventory")%>'></asp:Label></td>
                <td><asp:Label ID="LabelAmountInventory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountInventory")%>'></asp:Label></td>
                <td>
                    <%--<input type="text" id="InputNumDeff" value='<%#DataBinder.Eval(Container.DataItem, "NumDeff")%>' readonly="readonly" runat="server" style="width:100px;"/>--%>
                    <asp:Label ID="LabelNumDiff" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NumDiff")%>'></asp:Label>
                </td>
                <td>
                    <%--<input type="text" id="InputAmountDeff" value='<%#DataBinder.Eval(Container.DataItem, "AmountDeff")%>' readonly="readonly" runat="server" style="width:100px;"/>--%>
                    <asp:Label ID="LabelAmountDiff" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AmountDiff")%>'></asp:Label>
                </td>
                <td><asp:TextBox ID="TextBoxReason" runat="server" Width="50px" Text=""></asp:TextBox></td>
                <td><asp:TextBox ID="TextBoxComment" runat="server" Width="50px" Text=""></asp:TextBox></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle1">
                <td></td>
                <td><strong>合计：</strong></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td><asp:Label ID="LabelSum1" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum2" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum3" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum4" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum5" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum6" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum7" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum8" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum9" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum10" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum11" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelSum12" runat="server"></asp:Label></td>
                <td></td>
                <td></td>                
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">没有相关记录!</asp:Panel>
    </table>
    </asp:Panel>
 
    </div><!--box-outer END -->
    </div> <!--box-wrapper END --> 

</asp:Content>
