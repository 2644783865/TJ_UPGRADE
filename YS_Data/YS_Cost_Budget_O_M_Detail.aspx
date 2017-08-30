<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YS_Cost_Budget_O_M_Detail.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.master" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_O_M_Detail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    订单监控/分析明细
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript" language="javascript"> 
     function PurMAR(ContractNo,Marcode,Marname)
     {          
        var autonum=Math.round(10000*Math.random()); 
        window.open("YS_Order_Detail.aspx?ContractNo="+ContractNo+"&autonum="+autonum+"&Marname="+Marname+"&Marcode="+Marcode+"");
     }
    </script>

    <%--gridview样式--%>
    <style type="text/css">
        .bubufxPagerCss table
        {
            text-align: center;
            margin: auto;
        }
        .bubufxPagerCss table td
        {
            border: 0px;
            padding: 5px;
        }
        .bubufxPagerCss td
        {
            border-left: #ffffff 3px solid;
            border-right: #ffffff 3px solid;
            border-bottom: #ffffff 3px solid;
        }
        .bubufxPagerCss a
        {
            color: #231815;
            text-decoration: none;
            padding: 3px 6px 3px 6px;
            margin: 0 0 0 4px;
            text-align: center;
            border: 1px solid #ac1f24;
        }
        .bubufxPagerCss span
        {
            color: #fefefe;
            background-color: #ac1f24;
            padding: 3px 6px 3px 6px;
            margin: 0 0 0 4px;
            text-align: center;
            font-weight: bold;
            border: 1px solid #ac1f24;
        }
    </style>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lbl_fathername" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            预算合计：<asp:Label ID="lbl_total_BG" runat="server"></asp:Label>元
                        </td>
                        <td align="right">
                            订单合计：<asp:Label ID="lbl_total" runat="server"></asp:Label>元
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto; overflow-x: yes; overflow-y: hidden;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="GridView1_onrowdatabound" AllowPaging="True" EmptyDataText="没有明细记录！！"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10" CellPadding="4">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="YS_CODE" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            HeaderText="代码" />
                        <asp:BoundField DataField="YS_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            HeaderText="名称" />
                        <asp:BoundField DataField="YS_Union_Amount_BG" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" HeaderText="预算数量" DataFormatString="{0:F3}" />
                        <asp:BoundField DataField="YS_Average_Price_BG" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" HeaderText="预算单价(含税)" DataFormatString="{0:N3}" />
                        <asp:BoundField DataField="YS_MONEY_BG" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            HeaderText="预算金额" DataFormatString="{0:N3}" />
                        <asp:BoundField DataField="YS_Union_Amount" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            HeaderText="订单数量" DataFormatString="{0:F3}" />
                        <asp:BoundField DataField="YS_Average_Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            HeaderText="订单单价" DataFormatString="{0:N3}" />
                        <asp:BoundField DataField="YS_MONEY" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            HeaderText="订单金额" DataFormatString="{0:N3}" />
                    </Columns>
                    <PagerSettings FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PageButtonCount="5"
                        PreviousPageText="上一页" Mode="NumericFirstLast" />
                    <PagerStyle BorderColor="#66FF66" Font-Names="宋体" Font-Size="12px" HorizontalAlign="Center"
                        CssClass="bubufxPagerCss" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                </asp:GridView>
                <br>
            </div>
        </div>
    </div>
     <div>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 15" align="Left">
                    <img alt="" src="/YS_Data/LightSalmon.jpg" width="50px" height="15" />
                    <asp:Label ID="Label3" runat="Server" Text="表示订单达到预算90%" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/Yellow.jpg" width="50px" height="15" />
                    <asp:Label ID="Label2" runat="Server" Text="表示订单超过预算" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
