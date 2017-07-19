<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" EnableEventValidation = "false" AutoEventWireup="true" CodeBehind="TM_MS_Update_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_Update_View" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript">
//    window.onload=function()
//    {
//        var table=document.getElementById("<%=GridView1.ClientID%>");
//        if(table)
//        {
//            var tr=table.getElementsByTagName("tr");
//            for(var i=1;i<tr.length;i++)
//            {
//                var obj=tr[i].getElementsByTagName("td")[1].innerHTML;
//                if(obj.indexOf("BG")>0)
//                {
//                    tr[i].style.background="#FF9966";
//                    document.getElementById("<%=pkid.ClientID%>").value="1";
//                    for(var j=i+1;j<tr.length;j++)//多次变更且原来在原始数据中不存在的
//                    {
//                        if(tr[j].getElementsByTagName("td")[4].innerHTML==tr[i].getElementsByTagName("td")[4].innerHTML)
//                        {
//                            tr[i].style.display="none";
//                        }
//                    }
//                }
//                else
//                {
//                    document.getElementById("<%=pkid.ClientID%>").value="0";
//                }
//            }
//        }
//    } 
</script>
    <div class="box-wrapper">
        <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width:96%">
                    <tr>
                    <td style="width:10%">最新版制作明细</td>
                    <td style="width:18%">生产制号：
                       <asp:Label ID="tsa_id" runat="server" Width="100px"></asp:Label>
                       <input id="pkid" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                    </td>
                    <td style="width:18%">项目名称：
                        <asp:Label ID="proname" runat="server" Width="100px"></asp:Label>
                        <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                    </td>
                    <td style="width:18%">工程名称：
                        <asp:Label ID="engname" runat="server" Width="100px"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button ID="exportDetail" runat="server" Text="导出明细"  
                        OnClientClick="return confirm(&quot;确定导出吗？&quot;)" onclick="exportDetail_Click"/>
                    </td>
                    <td align="right">
                        <asp:Button ID="packlist" runat="server" Text="装箱单" Visible="false" 
                        OnClientClick="return confirm(&quot;确定生成装箱单？&quot;)" onclick="packlist_Click"/>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
        <div class="box-outer" runat="server">
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" Font-Size="Large">
            没有记录!</asp:Panel>
            <asp:Panel ID="Panel1" runat="server" Style="height: 560px; width: 100%; position: static">
            <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <%--<asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" 
                            runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true" 
                            onrowdatabound="GridView1_RowDataBound">--%>
                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MS_PID" HeaderText="批号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_INDEX" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_NAME" HeaderText="名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_UNUM" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_UWGHT" DataFormatString="{0:N2}" HeaderText="单重(kg)" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_TLWGHT" DataFormatString="{0:N2}" HeaderText="总重(kg)" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_MASHAPE" HeaderText="毛坯" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_MASTATE" HeaderText="状态" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_STANDARD" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_PROCESS" HeaderText="工艺流程" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_TIMERQ" HeaderText="箱号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MS_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableHeight="560px" TableWidth="100%" />
                <%--</asp:GridView>--%>
            </yyc:SmartGridView>
            </asp:Panel>
            <%--<asp:Panel ID="NoDataPanel" runat="server" Font-Size="Large" BackColor="#B3CDE8" ForeColor="Red">
            备注：红色标志为发生变更过的数据!</asp:Panel>--%>
        </div>
    </div>
</asp:Content>
