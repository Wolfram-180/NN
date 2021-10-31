object frm: Tfrm
  Left = 871
  Top = 428
  Width = 193
  Height = 121
  Caption = 'frm'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object connect1: TUniConnection
    ProviderName = 'MySQL'
    Port = 3306
    Database = 'nalice'
    SpecificOptions.Strings = (
      'MySQL.Protocol=mpTCP'
      'MySQL.ConnectionTimeout=60')
    Username = 'reader'
    Server = 'localhost'
    LoginPrompt = False
    Left = 40
    Top = 8
    EncryptedPassword = 
      'C8FFCAFF91FF99FF97FF90FF88FF86FFB5FFB7FFABFFB7FF98FFB7FF91FFA9FF' +
      'CAFF89FF99FF9BFF'
  end
  object qUSDJPY: TUniQuery
    Connection = connect1
    SQL.Strings = (
      'call get_forecast('#39'De4HjvDLbU'#39', '#39'USDJPY'#39')')
    Left = 88
    Top = 8
  end
  object qGBPUSD: TUniQuery
    Connection = connect1
    SQL.Strings = (
      'call get_forecast('#39'De4HjvDLbU'#39', '#39'GBPUSD'#39')')
    Left = 120
    Top = 8
  end
  object mysqlnprvdr1: TMySQLUniProvider
    Left = 56
    Top = 56
  end
end
